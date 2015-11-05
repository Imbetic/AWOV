using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    public float duration;
    public bool DestroyOnCollision;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if(duration <=0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (DestroyOnCollision)
        {
            Destroy(this.gameObject);
        }
    }
}
