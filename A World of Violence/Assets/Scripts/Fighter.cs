using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour
{

    CharacterActions Brain;

    public GameObject Weapon;
    public GameObject Unarmed;

    GameObject CurrentAttack;

    public float RunningAcceleration;
    public float Footing;

    bool PreviousJump;
    bool Grounded;
    public float JumpForce;

    public float Gravity;

    float JumpAscendDuration;
    public float MaxJumpAscendDuration;

    bool PreviousAttack;

    public float MaxAttackDuration;
    float AttackDuration;
    float DrawbackDuration;

    public float ChargeDuration;

    public int ChargeDirection; //0= up, 1 = right, 2 = down, 3 = left;
    public int PreviousChargeDirection;

    public float JumpInterval;
    public float JumpIntervalTimer;

    bool IsBlocking;

    // Use this for initialization
    void Start()
    {
        Brain = GetComponent<CharacterActions>();
    }

    //For isdownonces and preparations for FixedUpdate;
    void PreUpdate()
    {

        PreviousJump = Brain.jump;
        if (Brain.upAttack || Brain.downAttack || Brain.leftAttack || Brain.rightAttack)
        {
            ChargeDuration += Time.deltaTime;
            PreviousAttack = true;
        }
        else
        {
            ChargeDuration = 0;
            PreviousAttack = false;
        }
        Brain.UpdateCommands();
    }

    void FixedUpdate()
    {
        PreUpdate();

        Movement();

        Combat();
    }

    void Movement()
    {
        if (Brain.moveLeft)
        {
            //Body.AddForce(-RunningAcceleration * Vector2.right);
        }
        if (Brain.moveRight)
        {
            //Body.AddForce(RunningAcceleration * Vector2.right);
        }

        Jumping();

        Friction();
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
            if (Grounded)
            {
                JumpIntervalTimer = 0;

                //Body.AddForce(JumpForce * Vector2.up);
                //Body.gravityScale = 0;
                JumpAscendDuration = MaxJumpAscendDuration;
            }

            JumpIntervalTimer -= Time.deltaTime;
        }

        if (JumpAscendDuration > 0)
        {
            JumpAscendDuration -= Time.deltaTime;
            if (JumpAscendDuration <= 0 || !Brain.jump)
            {
                //Body.gravityScale = PersonalGravityScale;
                JumpAscendDuration = 0;
            }
        }
    }

    void Friction()
    {
        if (Grounded || Brain.moveLeft || Brain.moveRight)
        {
            float FrictionForceX = -Footing /** Body.mass * Body.velocity.x*/;
            if (FrictionForceX > RunningAcceleration) FrictionForceX = RunningAcceleration;
            //Body.AddForce(FrictionForceX * Vector2.right); //Friction
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (transform.position.y - GetComponent<BoxCollider2D>().size.y / 2 > other.transform.position.y + other.transform.localScale.y * other.gameObject.GetComponent<BoxCollider2D>().size.y / 2
                && transform.position.x + GetComponent<BoxCollider2D>().size.x / 2 > other.transform.position.x - other.transform.localScale.x * other.gameObject.GetComponent<BoxCollider2D>().size.x / 2
                && transform.position.x - GetComponent<BoxCollider2D>().size.x / 2 < other.transform.position.x + other.transform.localScale.x * other.gameObject.GetComponent<BoxCollider2D>().size.x / 2)
        {
            if (/*Body.velocity.y <= 0*/true)
            {
                JumpAscendDuration = MaxJumpAscendDuration;
                Grounded = true;
            }
        }

        //kollision under mark
        if (transform.position.y + GetComponent<BoxCollider2D>().size.y / 2 < other.transform.position.y - other.transform.localScale.y / 2 * other.gameObject.GetComponent<BoxCollider2D>().size.y / 2
            && transform.position.x + GetComponent<BoxCollider2D>().size.x / 2 > other.transform.position.x - other.transform.localScale.x * other.gameObject.GetComponent<BoxCollider2D>().size.x / 2
            && transform.position.x - GetComponent<BoxCollider2D>().size.x / 2 < other.transform.position.x + other.transform.localScale.x * other.gameObject.GetComponent<BoxCollider2D>().size.x / 2)
        {
            JumpAscendDuration = 0;
            //Body.gravityScale = PersonalGravityScale;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {

        Grounded = false;

    }

    void Combat()
    {
        Blocking();
        Attacking();
    }

    void Attacking()
    {
        if (AttackDuration > 0)
        {
            AttackDuration -= Time.deltaTime;
            if (AttackDuration <= 0)
            {
                CurrentAttack.SetActive(false);
                CurrentAttack = null;
            }

        }
        if (DrawbackDuration > 0)
        {
            DrawbackDuration -= Time.deltaTime;
        }
        else if(Brain.block)
        {
            ChargeDuration = 0;
        }
        else
        {
            GameObject tempwep;
            if (Weapon != null)
            {
                tempwep = Weapon;
            }
            else tempwep = Unarmed;

            if (Brain.upAttack)
            {
                if (!Brain.PreviousupAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 0;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().UpAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 0)
                {

                    if (PreviousChargeDirection == 1 && Brain.rightAttack)
                    {
                        ChargeDirection = 1;
                    }
                    else if (PreviousChargeDirection == 2 && Brain.downAttack)
                    {
                        ChargeDirection = 2;
                    }
                    else if (PreviousChargeDirection == 3 && Brain.leftAttack)
                    {
                        ChargeDirection = 3;
                    }
                    else
                    {
                        if (Brain.rightAttack)
                        {
                            ChargeDirection = 1;
                        }
                        else if (Brain.downAttack)
                        {
                            ChargeDirection = 2;
                        }
                        else if (Brain.leftAttack)
                        {
                            ChargeDirection = 3;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().UpAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().UpAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 0;
                }
            }

            if (Brain.downAttack)
            {
                if (!Brain.PreviousdownAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 2;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().DownAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 2)
                {

                    if (PreviousChargeDirection == 1 && Brain.rightAttack)
                    {
                        ChargeDirection = 1;
                    }
                    else if (PreviousChargeDirection == 0 && Brain.upAttack)
                    {
                        ChargeDirection = 0;
                    }
                    else if (PreviousChargeDirection == 3 && Brain.leftAttack)
                    {
                        ChargeDirection = 3;
                    }
                    else
                    {
                        if (Brain.rightAttack)
                        {
                            ChargeDirection = 1;
                        }
                        else if (Brain.upAttack)
                        {
                            ChargeDirection = 0;
                        }
                        else if (Brain.leftAttack)
                        {
                            ChargeDirection = 3;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().DownAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().DownAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 2;

                }
            }

            if (Brain.leftAttack)
            {
                if (!Brain.PreviousleftAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 3;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().BackAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 3)
                {

                    if (PreviousChargeDirection == 1 && Brain.rightAttack)
                    {
                        ChargeDirection = 1;
                    }
                    else if (PreviousChargeDirection == 0 && Brain.upAttack)
                    {
                        ChargeDirection = 0;
                    }
                    else if (PreviousChargeDirection == 2 && Brain.downAttack)
                    {
                        ChargeDirection = 2;
                    }
                    else
                    {
                        if (Brain.rightAttack)
                        {
                            ChargeDirection = 1;
                        }
                        else if (Brain.upAttack)
                        {
                            ChargeDirection = 0;
                        }
                        else if (Brain.downAttack)
                        {
                            ChargeDirection = 2;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().BackAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().BackAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }

                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 3;
                }
            }

            if (Brain.rightAttack)
            {
                if (!Brain.PreviousrightAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 1;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().FrontAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 1)
                {

                    if (PreviousChargeDirection == 3 && Brain.leftAttack)
                    {
                        ChargeDirection = 3;
                    }
                    else if (PreviousChargeDirection == 0 && Brain.upAttack)
                    {
                        ChargeDirection = 0;
                    }
                    else if (PreviousChargeDirection == 2 && Brain.downAttack)
                    {
                        ChargeDirection = 2;
                    }
                    else
                    {
                        if (Brain.leftAttack)
                        {
                            ChargeDirection = 3;
                        }
                        else if (Brain.upAttack)
                        {
                            ChargeDirection = 0;
                        }
                        else if (Brain.downAttack)
                        {
                            ChargeDirection = 2;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().FrontAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().FrontAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 1;
                }
            }
        }
    }

    void Blocking()
    {
        if (Brain.block)
        {
            if (DrawbackDuration < 0)
            {
                IsBlocking = true;
            }
        }
        else IsBlocking = false;
    }

}
