using UnityEngine;
using System.Collections;

public class LineCastPhysicsScript : MonoBehaviour
{

    public float Gravity;
    public float CurrentGravity;

    public Vector2 velocity;

    public bool Grounded;

    public LayerMask GroundCollision;
    public LayerMask DodgePlatformsCollision;
    LayerMask CurrentLayer;

    public bool DodgingPlatforms;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DodgingPlatforms)
        {
            CurrentLayer = DodgePlatformsCollision;
        }
        else CurrentLayer = GroundCollision;

        RaycastHit2D leftrayinfo = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y + velocity.y * Time.deltaTime), -Vector2.up, 0.5f, CurrentLayer);
        RaycastHit2D righttrayinfo = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y + velocity.y * Time.deltaTime), -Vector2.up, 0.5f, CurrentLayer);
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
                if (!Grounded && info.distance - velocity.y * Time.deltaTime >= 0.5f)
                {
                    transform.position += new Vector3(0, (velocity.y * Time.deltaTime) + 0.5f - info.distance, 0);
                    velocity = new Vector3(velocity.x, 0, 0);
                    Grounded = true;
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

        transform.position += new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);

        if (!Grounded)
        {
            velocity += CurrentGravity * Time.deltaTime * -Vector2.up;
        }



    }
}
