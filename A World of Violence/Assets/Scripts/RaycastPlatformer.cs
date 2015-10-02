using UnityEngine;
using System.Collections;

public class RaycastPlatformer : MonoBehaviour
{
    //The controller
    CharacterActions Brain;

    public Transform Visuals;
    public Animator Legs;

    public Fighter FightingScript;

    //Movement tweaking
    public bool BroClimb;
    public bool BroSwing;
    public int BonusJumps;
    public float Levitation;
    public float Quickfall;
    public float RunningAcceleration;
    public float Footing;
    public float JumpForce;
    public float Gravity = 20;
    public float MaxJumpAscendDuration;

    //Jumpingrelated
    bool PreviousBroSwinging = false;
    bool Broswinging = false;
    int BonusJumpCount;
    bool PreviousJump;
    public bool Grounded;
    public float JumpAscendDuration;
    public float JumpInterval;
    public float JumpIntervalTimer;

    //Collisionrelated
    private float tempX;
    private float tempY;
    private bool offsetedup = false;
    private bool offseteddown = false;
    private bool offsetedx = false;
    public LayerMask GroundCollision;
    public LayerMask DodgePlatformsCollision;
    LayerMask CurrentLayer;
    Vector2 temppos;
    public bool DodgingPlatforms;
    bool stopAtPlatform = false;
    public float playerheightdown;
    public float playerheightup;
    public float playerwidth;

    //Physics
    float CurrentGravity;
    public Vector2 velocity;
    Vector2 ForceToAdd;
    float DeltaTime;

    Vector2 rayDirection;

    void Start()
    {
        //Time.timeScale = 0.1f;
        CurrentGravity = Gravity;
        Brain = GetComponent<CharacterActions>();
    }

    void PreUpdate()
    {
        PreviousJump = Brain.jump;
        DeltaTime = Time.deltaTime;
        if (DeltaTime > 0.04f) DeltaTime = 0.04f;

        Brain.UpdateMovementCommands();
        if (FightingScript != null)
        {
            FightingScript.Fighting();
        }
    }

    void Update()
    {
        PreUpdate();

        Movement();
        velocity += (ForceToAdd);
        ForceToAdd *= 0;

        velocity += -Vector2.up * CurrentGravity * DeltaTime;
        if (JumpAscendDuration <= 0 && Brain.jump)
        {
            velocity += Vector2.up * Levitation * DeltaTime;
        }

        Friction();
        Positioning();
        if (!Grounded)
        {
            Legs.SetBool("Jumping", true);
        }
        else Legs.SetBool("Jumping", false);

    }

    void Movement()
    {
        Walking();
        Jumping();
    }

    void Walking()
    {
        Legs.SetBool("Running", false);
        
        if (FightingScript != null)
        {
            FightingScript.TheTorso.GetComponent<Animator>().SetBool("Running", false);
            Legs.SetBool("Backing", false);
            if (FightingScript.isAttacking)
            {
                if(FightingScript.CurrentAttack.GetComponent<AttackScript>().Direction == 1)
                {
                    Visuals.localScale = new Vector3(1, 1, 1);
                }
                else if (FightingScript.CurrentAttack.GetComponent<AttackScript>().Direction == 3)
                {
                    Visuals.localScale = new Vector3(-1, 1, 1);
                }

                if (Brain.moveLeft)
                {
                    velocity -= Vector2.right * DeltaTime * RunningAcceleration;
                    if(Visuals.localScale.x == 1)
                    {
                        Legs.SetBool("Backing", true);
                    }
                    else
                    {
                        Legs.SetBool("Running", true);
                    }
                }
                if (Brain.moveRight)
                {
                    velocity += Vector2.right * DeltaTime * RunningAcceleration;
                    if (Visuals.localScale.x == 1)
                    {
                        Legs.SetBool("Running", true);
                    }
                    else
                    {
                        Legs.SetBool("Backing", true);
                    }
                }
            }
            else if(FightingScript.ChargeDuration>0)
            {
                if(FightingScript.ChargeDirection == 1)
                {
                    Visuals.localScale = new Vector3(1, 1, 1);
                }
                
                if (FightingScript.ChargeDirection == 3)
                {
                    Visuals.localScale = new Vector3(-1, 1, 1);
                }

                if (Brain.moveLeft)
                {
                    velocity -= Vector2.right * DeltaTime * RunningAcceleration;
                    if (Visuals.localScale.x == 1)
                    {
                        Legs.SetBool("Backing", true);
                    }
                    else
                    {
                        Legs.SetBool("Running", true);
                    }
                }
                if (Brain.moveRight)
                {
                    velocity += Vector2.right * DeltaTime * RunningAcceleration;
                    if (Visuals.localScale.x == 1)
                    {
                        Legs.SetBool("Running", true);
                    }
                    else
                    {
                        Legs.SetBool("Backing", true);
                    }
                }
            }
            else
            {
                //FightingScript.TheTorso.GetComponent<Animator>().SetBool("CS1", false);
                if (Brain.moveLeft)
                {
                    //Body.AddForce(-RunningAcceleration * Vector2.right);
                    velocity -= Vector2.right * DeltaTime * RunningAcceleration;
                    //transform.localScale = new Vector3(-1, 1, 1);

                    Visuals.localScale = new Vector3(-1, 1, 1);
                    Legs.SetBool("Running", true);
                    FightingScript.TheTorso.GetComponent<Animator>().SetBool("Running", true);

                }
                if (Brain.moveRight)
                {
                    //Body.AddForce(RunningAcceleration * Vector2.right);
                    velocity += Vector2.right * DeltaTime * RunningAcceleration;
                    //transform.localScale = new Vector3(1, 1, 1);

                    Visuals.localScale = new Vector3(1, 1, 1);
                    Legs.SetBool("Running", true);
                    FightingScript.TheTorso.GetComponent<Animator>().SetBool("Running", true);
                }
            }
        }
        else
        {

            if (Brain.moveLeft)
            {
                velocity -= Vector2.right * DeltaTime * RunningAcceleration;

                Visuals.localScale = new Vector3(-1, 1, 1);
                Legs.SetBool("Running", true);
            }
            if (Brain.moveRight)
            {
                velocity += Vector2.right * DeltaTime * RunningAcceleration;

                Visuals.localScale = new Vector3(1, 1, 1);
                Legs.SetBool("Running", true);
            }

        }



        if (Brain.moveDown)
        {
            DodgingPlatforms = true;
        }
        else DodgingPlatforms = false;
    }

    void Jumping()
    {
        if (Brain.jump && !PreviousJump)
        {
            JumpIntervalTimer = JumpInterval;
        }
        else if (!Brain.jump)
        {
            JumpIntervalTimer = 0;
        }

        if (JumpIntervalTimer > 0)
        {

            Jump();

            JumpIntervalTimer -= DeltaTime;
        }

        if (JumpAscendDuration > 0)
        {
            JumpAscendDuration -= DeltaTime;
            if (JumpAscendDuration <= 0 || !Brain.jump)
            {
                CurrentGravity = Gravity;
                JumpAscendDuration = 0;
            }
        }
    }

    void Jump()
    {
        if (Grounded)
        {
            //BonusJumpCount = BonusJumps;
            JumpIntervalTimer = 0;
            velocity += Vector2.up * JumpForce;
            CurrentGravity = 0;
            JumpAscendDuration = MaxJumpAscendDuration;
        }
        else if (BonusJumpCount > 0)
        {
            BonusJumpCount -= 1;
            JumpIntervalTimer = 0;
            velocity = new Vector2(velocity.x, JumpForce);
            CurrentGravity = 0;
            JumpAscendDuration = MaxJumpAscendDuration;
        }
    }

    void Friction()
    {
        if (Grounded || Brain.moveLeft || Brain.moveRight)
        {
            //x-friction
            float FrictionForceX = -Footing * velocity.x;
            if (FrictionForceX > RunningAcceleration) FrictionForceX = RunningAcceleration;
            velocity += FrictionForceX * DeltaTime * Vector2.right;
        }
        if (velocity.y < 0)
        {
            //y-friction
            float FrictionForceY = -Footing * velocity.y;
            velocity += FrictionForceY * DeltaTime * Vector2.up / 3;
        }
    }

    void PrePositioning()
    {
        if (DodgingPlatforms)
        {
            CurrentLayer = DodgePlatformsCollision;
        }
        else CurrentLayer = GroundCollision;

        PreviousBroSwinging = Broswinging;
        Broswinging = false;

        offsetedup = false;
        offseteddown = false;
        offsetedx = false;
        Grounded = false;

        if (stopAtPlatform)
        {
            StopAtPlatformFix();
        }
    }

    public void StopAtPlatformFix()
    {
        temppos = new Vector2(transform.position.x, transform.position.y - playerheightdown);
        RaycastHit2D middownrayinfo = Physics2D.Raycast(temppos, new Vector2(0, 1), 0.5f, CurrentLayer);
        if (middownrayinfo.collider != null)
        {
            if (middownrayinfo.distance != 0)
            {
                transform.position += new Vector3(0, middownrayinfo.distance, 0);
            }
        }
        else
        {
            temppos = new Vector2(transform.position.x - playerwidth / 2, transform.position.y - playerheightdown);
            RaycastHit2D downleftrayinfo = Physics2D.Raycast(temppos, new Vector2(0, 1), 0.5f, CurrentLayer);
            if (downleftrayinfo.collider != null)
            {
                if (downleftrayinfo.distance != 0)
                {
                    transform.position += new Vector3(0, downleftrayinfo.distance, 0);
                }
            }
            else
            {
                temppos = new Vector2(transform.position.x + playerwidth / 2, transform.position.y - playerheightdown);
                RaycastHit2D downrightrayinfo = Physics2D.Raycast(temppos, new Vector2(0, 1), 0.5f, CurrentLayer);
                if (downrightrayinfo.collider != null)
                {
                    if (downrightrayinfo.distance != 0)
                    {
                        transform.position += new Vector3(0, downrightrayinfo.distance, 0);
                    }
                }
            }
        }
    }

    public void Positioning() //Called after all movement to adjust position by collisions
    {
        PrePositioning();

        rayDirection = velocity.normalized;

        temppos = new Vector2(transform.position.x, transform.position.y + playerheightup);
        RaycastHit2D miduprayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, DodgePlatformsCollision);

        temppos = new Vector2(transform.position.x, transform.position.y - playerheightdown);
        RaycastHit2D middownrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, CurrentLayer);

        temppos = new Vector2(transform.position.x - playerwidth / 2, transform.position.y);
        RaycastHit2D midleftrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, DodgePlatformsCollision);

        temppos = new Vector2(transform.position.x + playerwidth / 2, transform.position.y);
        RaycastHit2D midrightrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, DodgePlatformsCollision);

        MidSideCollisionCheck(miduprayinfo, middownrayinfo, midleftrayinfo, midrightrayinfo);

        CornerCollisionCheck(miduprayinfo, middownrayinfo, midleftrayinfo, midrightrayinfo);

        PostPositioning();
    }

    void MidSideCollisionCheck(RaycastHit2D miduprayinfo, RaycastHit2D middownrayinfo, RaycastHit2D midleftrayinfo, RaycastHit2D midrightrayinfo)
    {
        if (miduprayinfo.collider != null && velocity.y >= 0)
        {
            MidUpCollision(miduprayinfo.distance);
        }

        if (middownrayinfo.collider != null && velocity.y <= 0)
        {
            MidDownCollision(middownrayinfo.distance);
            stopAtPlatform = true;
        }
        else stopAtPlatform = false;

        if (midrightrayinfo.collider != null && velocity.x >= 0)
        {
            MidRightCollision(midrightrayinfo.distance);
        }

        if (midleftrayinfo.collider != null && velocity.x <= 0)
        {
            MidLeftCollision(midleftrayinfo.distance);
        }
    }

    void CornerCollisionCheck(RaycastHit2D miduprayinfo, RaycastHit2D middownrayinfo, RaycastHit2D midleftrayinfo, RaycastHit2D midrightrayinfo)
    {
        rayDirection = velocity.normalized;
        if (miduprayinfo.collider == null && midleftrayinfo.collider == null && (velocity.x < 0 || velocity.y > 0))
        {
            UpLeftCollisionCheck();
        }

        if (miduprayinfo.collider == null && midrightrayinfo.collider == null && (velocity.x > 0 || velocity.y > 0))
        {
            UpRightCollisionCheck();
        }

        if (middownrayinfo.collider == null && midleftrayinfo.collider == null && (velocity.x < 0 || velocity.y < 0))
        {
            DownLeftCollisionCheck();
        }

        if (middownrayinfo.collider == null && midrightrayinfo.collider == null && (velocity.x > 0 || velocity.y < 0))
        {
            DownRightCollisionCheck();
        }
    }

    void MidUpCollision(float raydist)
    {
        transform.position += new Vector3(0, raydist * rayDirection.y, 0);
        offseteddown = true;
        JumpAscendDuration = 0;
        CurrentGravity = Gravity;
        velocity = new Vector2(velocity.x, 0);
    }

    void MidDownCollision(float raydist)
    {
        transform.position += new Vector3(0, raydist * rayDirection.y, 0);
        offsetedup = true;
        Grounded = true;
        BonusJumpCount = BonusJumps;
        velocity = new Vector2(velocity.x, 0);
    }

    void MidRightCollision(float raydist)
    {
        transform.position += new Vector3(raydist * rayDirection.x, 0, 0);
        velocity = new Vector2(0, velocity.y);
        offsetedx = true;
    }

    void MidLeftCollision(float raydist)
    {
        transform.position += new Vector3(raydist * rayDirection.x, 0, 0);
        velocity = new Vector2(0, velocity.y);
        offsetedx = true;
    }

    void UpLeftCollisionCheck()
    {
        temppos = new Vector2(transform.position.x - playerwidth / 2, transform.position.y + playerheightup);
        RaycastHit2D upleftrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, DodgePlatformsCollision);

        if (upleftrayinfo.collider != null)
        {
            tempX = upleftrayinfo.point.x - upleftrayinfo.collider.transform.position.x;
            tempY = upleftrayinfo.point.y - upleftrayinfo.collider.transform.position.y;

            UpLeftCollision(upleftrayinfo);
        }
    }

    void UpLeftCollision(RaycastHit2D upleftrayinfo)
    {
        if (Mathf.Abs(tempX) > Mathf.Abs(tempY))
        {
            if (tempX > 0 && velocity.x < 0 && !offsetedx)
            {
                transform.position += new Vector3(upleftrayinfo.distance * rayDirection.x, 0, 0);
                //OFFSET RIGHT
                velocity = new Vector2(0, velocity.y);
                offsetedx = true;
            }
        }
        else
        {
            if (tempY < 0 && !offseteddown && velocity.y > 0)
            {

                transform.position += new Vector3(0, upleftrayinfo.distance * rayDirection.y, 0);
                //OFFSET DOWN
                offseteddown = true;
                //JumpAscendDuration = 0;
                //CurrentGravity = Gravity;
                //velocity = new Vector2(velocity.x, 0);
                if (Brain.jump && !Brain.moveLeft && BroSwing)
                {
                    Broswinging = true;
                    Grounded = true;
                    BonusJumpCount = BonusJumps;
                    velocity = new Vector2(5, 0);
                    CurrentGravity = 0;
                }
                else
                {
                    JumpAscendDuration = 0;
                    CurrentGravity = Gravity;
                    velocity = new Vector2(velocity.x, 0);
                }

            }
        }
    }

    void UpRightCollisionCheck()
    {
        temppos = new Vector2(transform.position.x + playerwidth / 2, transform.position.y + playerheightup);
        RaycastHit2D uprightrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, DodgePlatformsCollision);

        if (uprightrayinfo.collider != null)
        {

            tempX = uprightrayinfo.point.x - uprightrayinfo.collider.transform.position.x;
            tempY = uprightrayinfo.point.y - uprightrayinfo.collider.transform.position.y;

            UpRightCollision(uprightrayinfo);
        }
    }

    void UpRightCollision(RaycastHit2D uprightrayinfo)
    {
        if (Mathf.Abs(tempX) > Mathf.Abs(tempY))
        {
            if (tempX < 0 && !offsetedx && velocity.x > 0)
            {
                //OFFSET LEFT
                transform.position += new Vector3(uprightrayinfo.distance * rayDirection.x, 0, 0);
                velocity = new Vector2(0, velocity.y);
                offsetedx = true;
            }
        }
        else
        {
            if (tempY < 0 && !offseteddown && velocity.y > 0)
            {
                transform.position += new Vector3(0, uprightrayinfo.distance * rayDirection.y, 0);
                //OFFSET DOWN
                offseteddown = true;
                if (Brain.jump && !Brain.moveRight && BroSwing)
                {
                    Broswinging = true;
                    Grounded = true;
                    BonusJumpCount = BonusJumps;
                    velocity = new Vector2(-5, 0);
                    CurrentGravity = 0;
                }
                else
                {
                    JumpAscendDuration = 0;
                    CurrentGravity = Gravity;
                    velocity = new Vector2(velocity.x, 0);
                }
            }
        }
    }

    void DownLeftCollisionCheck()
    {
        temppos = new Vector2(transform.position.x - playerwidth / 2, transform.position.y - playerheightdown);
        RaycastHit2D downleftrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, CurrentLayer);

        if (downleftrayinfo.collider != null)
        {

            tempX = downleftrayinfo.point.x - downleftrayinfo.collider.transform.position.x;
            tempY = downleftrayinfo.point.y - downleftrayinfo.collider.transform.position.y;

            DownLeftCollision(downleftrayinfo);

        }
    }

    void DownLeftCollision(RaycastHit2D downleftrayinfo)
    {
        if (Mathf.Abs(tempX) > Mathf.Abs(tempY))
        {
            if (tempX > 0 && !offsetedx && velocity.x < 0)
            {
                //OFFSET RIGHT
                if (downleftrayinfo.collider.gameObject.layer != 1 << 9)
                {
                    transform.position += new Vector3(downleftrayinfo.distance * rayDirection.x, 0, 0);
                }

                if (Brain.moveLeft && JumpAscendDuration <= 0 && BroClimb)
                {
                    velocity = new Vector2(0, 4);
                }
                else
                {
                    velocity = new Vector2(0, velocity.y);
                }
                offsetedx = true;
            }
        }
        else
        {
            if (tempY > 0 && velocity.y < 0 && !offsetedup)
            {
                transform.position += new Vector3(0, downleftrayinfo.distance * rayDirection.y, 0);
                stopAtPlatform = true;
                //OFFSET UP
                offsetedup = true;
                Grounded = true;
                BonusJumpCount = BonusJumps;
                velocity = new Vector2(velocity.x, 0);

            }
            else stopAtPlatform = false;
        }
    }

    void DownRightCollisionCheck()
    {
        temppos = new Vector2(transform.position.x + playerwidth / 2, transform.position.y - playerheightdown);
        RaycastHit2D downrightrayinfo = Physics2D.Raycast(temppos, rayDirection, (velocity * DeltaTime).magnitude, CurrentLayer);

        if (downrightrayinfo.collider != null)
        {
            tempX = downrightrayinfo.point.x - downrightrayinfo.collider.transform.position.x;
            tempY = downrightrayinfo.point.y - downrightrayinfo.collider.transform.position.y;

            DownRightCollision(downrightrayinfo);
        }
    }

    void DownRightCollision(RaycastHit2D downrightrayinfo)
    {
        if (Mathf.Abs(tempX) > Mathf.Abs(tempY))
        {
            if (tempX < 0 && !offsetedx && velocity.x > 0)
            {
                //OFFSET LEFT
                if (downrightrayinfo.collider.gameObject.layer != 1 << 9)
                {
                    transform.position += new Vector3(downrightrayinfo.distance * rayDirection.x, 0, 0);
                }

                if (Brain.moveRight && JumpAscendDuration <= 0 && BroClimb)
                {
                    velocity = new Vector2(0, 4);
                }
                else
                {
                    velocity = new Vector2(0, velocity.y);
                }

                offsetedx = true;

            }
        }
        else
        {
            if (tempY > 0 && !offsetedup && velocity.y < 0)
            {
                transform.position += new Vector3(0, downrightrayinfo.distance * rayDirection.y, 0);
                //OFFSET UP
                stopAtPlatform = true;
                Grounded = true;
                BonusJumpCount = BonusJumps;
                offsetedup = true;
                velocity = new Vector2(velocity.x, 0);

            }
            else stopAtPlatform = false;
        }
    }

    void PostPositioning()
    {
        if (PreviousBroSwinging && !Broswinging)
        {
            Grounded = true;
            BonusJumpCount = BonusJumps;
            JumpIntervalTimer = JumpInterval;
            velocity = new Vector2(velocity.x, 0);
            //velocity = new Vector2(velocity.x, 10);
        }

        transform.position += new Vector3(velocity.x, velocity.y, 0) * DeltaTime;

        if (Broswinging)
        {
            velocity = new Vector2(velocity.x, 10);
        }
    }

    public void AddForce(Vector2 force)
    {
        ForceToAdd += force;
    }
}
