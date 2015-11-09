using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CharacterPartButton : MonoBehaviour
{

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

    CharacterProperties TheCharacter;

    public GameObject NextOptionState;
    public GameObject PreviousOptionState;
    public GameObject Colors;
 
    public Transform AllColors;


    public bool isChest;
    public bool pushedBoobs;

    void Start()
    {
        TheCharacter = GameObject.Find("Character").GetComponent<CharacterProperties>();
    }

    public void ChangeSprite()
    {
        if (TheCharacter.female)
        {
            if (isChest)
            {
                TheCharacter.SetPushedBoobs(pushedBoobs);
            }
            
        }
        CharacterPart.sprite = NewSprite;
        for (int i = 0; i < AllColors.childCount; i++) {
            AllColors.GetChild(i).gameObject.SetActive(false);
        }
            Colors.SetActive(true);

    }

    void OnEnable()
    {
        TheCharacter = GameObject.Find("Character").GetComponent<CharacterProperties>();

        if (TheCharacter.female)
        {
            if (TheCharacter.bodyType == 0)
            {
                NewSprite = f_sprite;
                //TheCharacter.pendingBodyType = bodyType;
            }
            else if (TheCharacter.bodyType == 1)
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
                //TheCharacter.pendingBodyType = bodyType;
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
