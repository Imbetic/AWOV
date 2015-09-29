using UnityEngine;
using System.Collections;

public class ParticleSource : MonoBehaviour
{

    public GameObject Sparks;


    float timer = 0.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
        //if (timer <= 0)
        //{
        //    CreateParticles(10, new Vector2(0, 0));
        //    timer = 0.5f;
        //}
    }

    public void CreateParticles(int quantity, Vector2 forcetoadd)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject tempparticle = Instantiate(Sparks);
            tempparticle.transform.position = new Vector3(transform.position.x + (float)Random.Range(-50, 50) / 100, transform.position.y + (float)Random.Range(-50, 50) / 100, transform.position.z);
            tempparticle.transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.zero, forcetoadd));
            tempparticle.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcetoadd.x + (float)Random.Range(-50, 50), forcetoadd.y + (float)Random.Range(-50, 50)), ForceMode2D.Impulse);
        };
    }

}