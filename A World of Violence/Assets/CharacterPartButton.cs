using UnityEngine;
using System.Collections;

public class CharacterPartButton : MonoBehaviour {

    public SpriteRenderer CharacterPart;

    public Sprite NewSprite;

    CharacterProperties TheCharacter;

	void Start () 
    {
        TheCharacter = GameObject.Find("Character").GetComponent<CharacterProperties>();
	}


    void ChangeSprite()
    {
        CharacterPart.sprite = NewSprite;
    }



}
