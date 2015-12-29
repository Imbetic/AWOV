using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Killable : MonoBehaviour
{

    public int health;
    float FreezeTimer;
    
    public UnityEvent onDamaged;

    void Update()
    {
        if (FreezeTimer > 0)
        {
            Time.timeScale = 0.1f;
            FreezeTimer -= Time.deltaTime;
            if (FreezeTimer <= 0)
            {
                Time.timeScale = 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<AttackScript>() != null)
        {
            if (GetComponent<RaycastPlatformer>() != null)
            {
                if (other.GetComponent<AttackScript>().Direction == 0)
                {
                    GetComponent<RaycastPlatformer>().velocity = Vector2.up * 15;
                }
                else if (other.GetComponent<AttackScript>().Direction == 1)
                {
                    GetComponent<RaycastPlatformer>().velocity = Vector2.right * 15;
                }
                else if (other.GetComponent<AttackScript>().Direction == 2)
                {
                    GetComponent<RaycastPlatformer>().velocity = Vector2.up * -15;
                }
                else if (other.GetComponent<AttackScript>().Direction == 3)
                {
                    GetComponent<RaycastPlatformer>().velocity = Vector2.right * -15;
                }
            }

            onDamaged.Invoke();

            health -= (int)other.GetComponent<AttackScript>().AttackDamage;
            GetComponent<ParticleSource>().CreateParticles(10, Vector2.zero);
            FreezeTimer = 0.05f;
            if(health <= 0)
            {
                GetComponent<ParticleSource>().CreateParticles(100, Vector2.zero);
                Destroy(this.gameObject);
            }
        }
        
    }


}
