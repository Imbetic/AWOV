using UnityEngine;
using System.Collections;

public class LineCastPhysicsScript : MonoBehaviour
{

    public float Gravity;
    public float CurrentGravity;

    public Vector2 velocity;
    float NextYVelocity;

    public bool Grounded;

    public LayerMask GroundCollision;
    public LayerMask DodgePlatformsCollision;
    LayerMask CurrentLayer;

    public bool DodgingPlatforms;
    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        //DownPhysics();
        //UpPhysics();

        //transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);

        //if (!Grounded)
        //{
        //    velocity += CurrentGravity * Time.deltaTime * -Vector2.up;
        //}

    }

    public void DownPhysics()
    {
        if (DodgingPlatforms)
        {
            CurrentLayer = DodgePlatformsCollision;
        }
        else CurrentLayer = GroundCollision;


        

        RaycastHit2D leftrayinfo = Physics2D.Raycast(new Vector2(transform.position.x + velocity.x * Time.deltaTime - 0.5f, transform.position.y + NextYVelocity), -Vector2.up, 0.5f, CurrentLayer);
        RaycastHit2D righttrayinfo = Physics2D.Raycast(new Vector2(transform.position.x + velocity.x * Time.deltaTime + 0.5f, transform.position.y + NextYVelocity), -Vector2.up, 0.5f, CurrentLayer);
        //Debug.DrawLine(new Vector2(transform.position.x - 0.5f, transform.position.y), new Vector2(transform.position.x - 0.5f, transform.position.y - 1f), Color.white);
        if (leftrayinfo || righttrayinfo)
        {
            RaycastHit2D info;
            if (leftrayinfo)
            {
                info = leftrayinfo;
            }
            else info = righttrayinfo;

            if (info.distance < 0.5f)
            {
                if (!Grounded && info.distance - (NextYVelocity) >= 0.5f)
                {
                    transform.position += new Vector3(0, (NextYVelocity) + 0.5f - info.distance, 0);
                    velocity = new Vector3(velocity.x, 0, 0);
                    Grounded = true;
                    //CurrentGravity = 0;

                }
                else
                {
                    Grounded = false;
                }
            }

        }
        else
        {
            Grounded = false;
        }

        
    }

    public bool UpPhysics()
    {

        RaycastHit2D leftrayinfo = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y + NextYVelocity), Vector2.up, 0.5f, DodgePlatformsCollision);
        RaycastHit2D righttrayinfo = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y + NextYVelocity), Vector2.up, 0.5f, DodgePlatformsCollision);
        //Debug.DrawLine(new Vector2(transform.position.x - 0.5f, transform.position.y), new Vector2(transform.position.x - 0.5f, transform.position.y - 1f), Color.white);
        if (leftrayinfo || righttrayinfo)
        {
            RaycastHit2D info;
            if (leftrayinfo)
            {
                info = leftrayinfo;
            }
            else info = righttrayinfo;

            if (info.distance < 0.5f && NextYVelocity > 0)
            {
                transform.position += new Vector3(0, (info.distance + NextYVelocity) - 0.5f /* + Gravity * Time.deltaTime * Time.deltaTime*/, 0);
                velocity = new Vector3(velocity.x, 0, 0);
                return true;
            }

        }
        return false;
    }

    public void GravityStuff()
    {
            velocity += CurrentGravity * Time.deltaTime * -Vector2.up;
    }

    public void VelocityUpdate()
    {
        transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);
    }

    public void CalculateNextYVelocity(float Footing)
    {
        if (Grounded)
        {
            NextYVelocity = velocity.y * Time.deltaTime;
        }
        else NextYVelocity = (velocity.y - CurrentGravity * Time.deltaTime) * Time.deltaTime;
    }

}
