using UnityEngine;
using System.Collections;

public class ChangeSkinColorButton : MonoBehaviour {

    public int skinColor;

    public SpriteRenderer body;

    public Sprite mPowerful;
    public Sprite mAthletic;
    public Sprite mFair;
    public Sprite mMighty;

    public Sprite fPowerful;
    public Sprite fAthletic;
    public Sprite fFair;
    public Sprite fMighty;

    CharacterProperties characterProperties;

    // Use this for initialization
    void Start()
    {
        characterProperties = GameObject.Find("Character").GetComponent<CharacterProperties>();
    }

    public void ChangeSkinColor()
    {
        characterProperties.skinColor = skinColor;
        if (characterProperties.female)
        {
            if (characterProperties.bodyType == 1)
            {
                characterProperties.unpushedBoobs = body.sprite = fPowerful;
            }
            else if (characterProperties.bodyType == 2)
            {
                characterProperties.unpushedBoobs = body.sprite = fAthletic;
            }
            else if (characterProperties.bodyType == 3)
            {
                characterProperties.unpushedBoobs = body.sprite = fFair;
            }
            else if (characterProperties.bodyType == 4)
            {
                characterProperties.unpushedBoobs = body.sprite = fMighty;
            }
        }
        else
        {
            if (characterProperties.bodyType == 1)
            {
                body.sprite = mPowerful;
            }
            else if (characterProperties.bodyType == 2)
            {
                body.sprite = mAthletic;
            }
            else if (characterProperties.bodyType == 3)
            {
                body.sprite = mFair;
            }
            else if (characterProperties.bodyType == 4)
            {
                body.sprite = mMighty;
            }
        }
    }
}
