using UnityEngine;
using System.Collections;

public class SexButton : MonoBehaviour {

    public bool isFemale;
	
	// Update is called once per frame
	public void SetSex()
    {
        GameObject.Find("Character").GetComponent<CharacterProperties>().female = isFemale;
	}
}
