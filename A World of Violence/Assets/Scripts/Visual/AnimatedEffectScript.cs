using UnityEngine;
using System.Collections;

public class AnimatedEffectScript : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Destroy(this.gameObject);
        }
	}
}
