using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterPartButton : MonoBehaviour {

    public SpriteRenderer CharacterPart;

    public Sprite NewSprite;

    public Sprite f_sprite;
    public Sprite m_sprite;

    public Sprite m_p_sprite;
    public Sprite m_a_sprite;
    public Sprite m_f_sprite;
    public Sprite m_m_sprite;

    public Sprite f_p_sprite;
    public Sprite f_a_sprite;
    public Sprite f_f_sprite;
    public Sprite f_m_sprite;

    public Sprite p_f_p_sprite;
    public Sprite p_f_a_sprite;
    public Sprite p_f_f_sprite;
    public Sprite p_f_m_sprite;

    public int bodyType; // Powerful, Athletic, Fair, Mighty
    public int skinColor; // Pale, Bright, Tanned, Dark

    CharacterProperties TheCharacter;

    public GameObject NextOptionState;
    public GameObject PreviousOptionState;

    public bool female;

    public bool resetSprites;

    public bool canContinue;

    public bool isChest;
    public bool pushedBoobs;

	void Start () 
    {
        TheCharacter = GameObject.Find("Character").GetComponent<CharacterProperties>();
	}


    public void ChangeSprite()
    {
        bool condition = false;
        if (TheCharacter.female)
        {
            if (skinColor != 0)
            {
                
                if(TheCharacter.Body.sprite == TheCharacter.pushedBoobs)
                {
                    condition = true;
                }
                TheCharacter.unpushedBoobs = NewSprite;
                if (TheCharacter.bodyType == 1)
                {
                    TheCharacter.pushedBoobs = p_f_p_sprite;
                }
                else if (TheCharacter.bodyType == 2)
                {
                    TheCharacter.pushedBoobs = p_f_a_sprite;
                }
                else if (TheCharacter.bodyType == 3)
                {
                    TheCharacter.pushedBoobs = p_f_f_sprite;
                }
                else if (TheCharacter.bodyType == 4)
                {
                    TheCharacter.pushedBoobs = p_f_m_sprite;
                }
            }
            if (isChest)
            {

                TheCharacter.SetPushedBoobs(pushedBoobs);

            }
            if(condition)
            {
                CharacterPart.sprite = TheCharacter.pushedBoobs;
            }
            else
            {
                CharacterPart.sprite = NewSprite;
            }
        }
        else CharacterPart.sprite = NewSprite;
        TheCharacter.readyToProceed = true;
        
        TheCharacter.pendingBodyType = bodyType;
        TheCharacter.pendingSkinColor = skinColor;
        
    }

    public void SetSex()
    {
        TheCharacter.readyToProceed = true;
        TheCharacter.female = female;
    }

    public void SetBodyType()
    {
        //TheCharacter.readyToProceed = true;
        TheCharacter.bodyType = TheCharacter.pendingBodyType;
        
    }

    public void SetSkinColor()
    {
        if (TheCharacter.skinColor == 0)
        {
            TheCharacter.skinColor = 2;
            TheCharacter.readyToProceed = true;
            if (TheCharacter.bodyType == 1)
            {
                TheCharacter.unpushedBoobs = f_p_sprite;
                TheCharacter.pushedBoobs = p_f_p_sprite;
            }
            else if (TheCharacter.bodyType == 2)
            {
                TheCharacter.unpushedBoobs = f_a_sprite;
                TheCharacter.pushedBoobs = p_f_a_sprite;
            }
            else if (TheCharacter.bodyType == 3)
            {
                TheCharacter.unpushedBoobs = f_f_sprite;
                TheCharacter.pushedBoobs = p_f_f_sprite;
            }
            else if (TheCharacter.bodyType == 4)
            {
                TheCharacter.unpushedBoobs = f_m_sprite;
                TheCharacter.pushedBoobs = p_f_m_sprite;
            }

        }
        TheCharacter.skinColor = TheCharacter.pendingSkinColor;
    }

    public void PreviousOption()
    {
        PreviousOptionState.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        
    }

    public void DisableChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    public void ResetCharacter()
    {
        TheCharacter.ResetProperties(resetSprites);
    }

    public void Next()
    {
        if (TheCharacter.readyToProceed)
        {
            TheCharacter.readyToProceed = false;
            NextOptionState.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        TheCharacter = GameObject.Find("Character").GetComponent<CharacterProperties>();
        if(canContinue)
        {
            TheCharacter.readyToProceed = true;
        }
        else TheCharacter.readyToProceed = false;

        if(TheCharacter.female)
        {
            if (TheCharacter.bodyType == 0)
            {
                NewSprite = f_sprite;
                TheCharacter.pendingBodyType = bodyType;
            }
            else if(TheCharacter.bodyType == 1)
            {
                NewSprite = f_p_sprite;
            }
            else if (TheCharacter.bodyType == 2)
            {
                NewSprite = f_a_sprite;
            }
            else if (TheCharacter.bodyType == 3)
            {
                NewSprite = f_f_sprite;
            }
            else if (TheCharacter.bodyType == 4)
            {
                NewSprite = f_m_sprite;
            }
        }
        else 
        {
            if (TheCharacter.bodyType == 0)
            {
                NewSprite = m_sprite;
                TheCharacter.pendingBodyType = bodyType;
            }
            else if (TheCharacter.bodyType == 1)
            {
                NewSprite = m_p_sprite;
            }
            else if (TheCharacter.bodyType == 2)
            {
                NewSprite = m_a_sprite;
            }
            else if (TheCharacter.bodyType == 3)
            {
                NewSprite = m_f_sprite;
            }
            else if (TheCharacter.bodyType == 4)
            {
                NewSprite = m_m_sprite;
            }
        }
    }
}
