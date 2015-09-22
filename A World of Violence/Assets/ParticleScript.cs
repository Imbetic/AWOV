using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    public float duration;

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
}
