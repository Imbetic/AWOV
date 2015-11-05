using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{

    public GameObject FrontAttack1;
    public GameObject BackAttack1;
    public GameObject UpAttack1;
    public GameObject DownAttack1;

    public GameObject FrontAttack2;
    public GameObject BackAttack2;
    public GameObject UpAttack2;
    public GameObject DownAttack2;

    public GameObject FrontAttack3;
    public GameObject BackAttack3;
    public GameObject UpAttack3;
    public GameObject DownAttack3;

    public float Attack2ChargeThreshold;
    public float Attack3ChargeThreshold;

    public Sprite IdleRightSprite;
    public Sprite VerticalAttack1Sprite;
    public Sprite UpAttack1Sprite;
    public Sprite DownAttack1Sprite;
    

    //Fighter Owner;

    // Use this for initialization
    void Start()
    {
        //Owner = transform.parent.GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
