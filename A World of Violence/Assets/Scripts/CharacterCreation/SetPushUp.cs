using UnityEngine;
using System.Collections;

public class SetPushUp : MonoBehaviour
{
    CharacterProperties characterProperties;

    public Sprite palePowerful;
    public Sprite paleAthletic;
    public Sprite paleFair;
    public Sprite paleMighty;

    public Sprite brightPowerful;
    public Sprite brightAthletic;
    public Sprite brightFair;
    public Sprite brightMighty;

    public Sprite tannedPowerful;
    public Sprite tannedAthletic;
    public Sprite tannedFair;
    public Sprite tannedMighty;

    public Sprite darkPowerful;
    public Sprite darkAthletic;
    public Sprite darkFair;
    public Sprite darkMighty;

    void Start()
    {
        characterProperties = GameObject.Find("Character").GetComponent<CharacterProperties>();
    }

    public void SetPushUpSprite()
    {
        if(characterProperties.female)
        {
            if(characterProperties.bodyType == 1)
            {
                if(characterProperties.skinColor == 1)
                {
                    characterProperties.pushedBoobs = palePowerful;
                }
                else if (characterProperties.skinColor == 2)
                {
                    characterProperties.pushedBoobs = brightPowerful;
                }
                else if (characterProperties.skinColor == 3)
                {
                    characterProperties.pushedBoobs = tannedPowerful;
                }
                else if (characterProperties.skinColor == 4)
                {
                    characterProperties.pushedBoobs = darkPowerful;
                }
            }
            else if (characterProperties.bodyType == 2)
            {
                if (characterProperties.skinColor == 1)
                {
                    characterProperties.pushedBoobs = paleAthletic;
                }
                else if (characterProperties.skinColor == 2)
                {
                    characterProperties.pushedBoobs = brightAthletic;
                }
                else if (characterProperties.skinColor == 3)
                {
                    characterProperties.pushedBoobs = tannedAthletic;
                }
                else if (characterProperties.skinColor == 4)
                {
                    characterProperties.pushedBoobs = darkAthletic;
                }
            }
            else if (characterProperties.bodyType == 3)
            {
                if (characterProperties.skinColor == 1)
                {
                    characterProperties.pushedBoobs = paleFair;
                }
                else if (characterProperties.skinColor == 2)
                {
                    characterProperties.pushedBoobs = brightFair;
                }
                else if (characterProperties.skinColor == 3)
                {
                    characterProperties.pushedBoobs = tannedFair;
                }
                else if (characterProperties.skinColor == 4)
                {
                    characterProperties.pushedBoobs = darkFair;
                }
            }
            else if (characterProperties.bodyType == 4)
            {
                if (characterProperties.skinColor == 1)
                {
                    characterProperties.pushedBoobs = paleMighty;
                }
                else if (characterProperties.skinColor == 2)
                {
                    characterProperties.pushedBoobs = brightMighty;
                }
                else if (characterProperties.skinColor == 3)
                {
                    characterProperties.pushedBoobs = tannedMighty;
                }
                else if (characterProperties.skinColor == 4)
                {
                    characterProperties.pushedBoobs = darkMighty;
                }
            }
        }
    }
}
