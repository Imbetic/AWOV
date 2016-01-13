using UnityEngine;
using System.Collections;

public class ManEaterBat : MonoBehaviour
{

    public Transform Target;
    Rigidbody2D theRigidBody;
    public float AttackDistance;
    // Use this for initialization

    public float attackTimer = 0;
    public bool attacking = false;
    float attackDelay;
    float previousDX;

    public GameObject visuals;

    void Start()
    {
        theRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dX = Target.position.x - transform.position.x;
        if (dX < 0)
        {
            visuals.transform.localScale = new Vector3(1, 1, 1);
            if (previousDX >= 0)
            {
                visuals.GetComponent<Animator>().SetBool("Attacking", false);
            }
        }
        else
        {
            visuals.transform.localScale = new Vector3(-1, 1, 1);
            if (previousDX < 0)
            {
                visuals.GetComponent<Animator>().SetBool("Attacking", false);
            }
        }
        if (!attacking)
        {
            
            //if (dX < 0)
            //{
            //    visuals.transform.localScale = new Vector3(1, 1, 1);
            //}
            //else
            //{
            //    visuals.transform.localScale = new Vector3(-1, 1, 1);
            //}
        }
        previousDX = dX;

        float dY = Target.position.y - transform.position.y;

        float distance = Mathf.Sqrt((dX * dX) + (dY * dY));

        if (attacking)
        {
            attackDelay -= Time.deltaTime;
            theRigidBody.velocity *= Mathf.Pow(0.4f, Time.deltaTime);
            if (attackDelay <= 0)
            {
                theRigidBody.velocity = (new Vector2(7f * dX / distance, 7f * dY / distance));
                attacking = false;
                visuals.GetComponent<Animator>().SetBool("Attacking", true);
                visuals.GetComponent<Animator>().SetBool("Preparing", false);
            }
        }
        else
        {
            {


                theRigidBody.AddForce(new Vector2(6f * dX / distance, 6f * dY / distance));

                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;

                    if (attackTimer <= 0)
                    {
                        if (AttackDistance <= distance)
                        {
                            attackTimer = 0;
                            visuals.GetComponent<Animator>().SetBool("Attacking", false);
                            visuals.GetComponent<Animator>().SetBool("Preparing", false);
                        }
                        else
                        {
                            attacking = true;
                            visuals.GetComponent<Animator>().SetBool("Attacking", false);
                            visuals.GetComponent<Animator>().SetBool("Preparing", true);
                            attackDelay = 1;                           
                        }
                        //theRigidBody.velocity *= Mathf.Pow(0.01f, Time.deltaTime);
                    }
                }
                else if (distance < AttackDistance)
                {
                    attackTimer = (float)Random.Range(0, 300) / 100;
                }
            }
        }
        theRigidBody.velocity *= Mathf.Pow(0.5f, Time.deltaTime);
        
    }

    public void OnDamaged()
    {
        attackTimer = 0;
        attacking = false;
        visuals.GetComponent<Animator>().SetBool("Preparing", false);
        if (visuals.GetComponent<Animator>().GetBool("Attacking"))
        {
            theRigidBody.velocity *= -1;
            visuals.GetComponent<Animator>().SetBool("Attacking", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        

        visuals.GetComponent<Animator>().SetBool("Attacking", false);
        
        
    }

}
