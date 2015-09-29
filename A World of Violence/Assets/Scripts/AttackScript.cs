using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    public float AttackDuration;
    public float AttackDrawBack;
    public float AttackDamage;
    public float AttackDelay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
