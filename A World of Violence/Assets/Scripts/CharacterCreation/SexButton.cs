using UnityEngine;
using System.Collections;

public class SexButton : MonoBehaviour {

    public bool isFemale;
    public NextButton next;
	
	// Update is called once per frame
	public void SetSex()
    {
        GameObject.Find("Character").GetComponent<CharacterProperties>().female = isFemale;
        next.ownedbutton.enabled = true;
	}
}
