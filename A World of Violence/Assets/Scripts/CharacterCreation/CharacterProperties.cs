using UnityEngine;
using System.Collections;

public class CharacterProperties : MonoBehaviour {

    public bool female;
    public int bodyType; // Powerful, Athletic, Fair, Mighty
    //public int pendingBodyType;
    public int skinColor = 2;
    //public int pendingSkinColor;
    public bool readyToProceed=false;

    public Sprite pushedBoobs;
    public Sprite unpushedBoobs;
    public SpriteRenderer Body;
	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    public void ResetProperties(bool resetSprites)
    {
        if(resetSprites)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        bodyType = 0;
        skinColor = 2;
    }

    public void SetPushedBoobs(bool ispushed)
    {
        if(ispushed)
        {
            Body.sprite = pushedBoobs;
        }
        else
        {
            Body.sprite = unpushedBoobs;
        }
    }
}
