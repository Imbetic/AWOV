using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

    public float AttackDuration;
    public float AttackDrawBack;
    public float AttackDamage;
    public float AttackDelay;
    public int Direction;
    public int Cleave;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Cleave -= 1;
        if (Cleave <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

            GetComponent<BoxCollider2D>().enabled = false;

    }
}
