using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BodyTypeButton : MonoBehaviour {

    public int bodyType;

    public Sprite femaleIcon;
    public Sprite femalePressed;

    public Sprite maleIcon;
    public Sprite malePressed;

    public SpriteRenderer body;

    public Sprite mPale;
    public Sprite mBright;
    public Sprite mTanned;
    public Sprite mDark;

    public Sprite fPale;
    public Sprite fBright;
    public Sprite fTanned;
    public Sprite fDark;

    CharacterProperties characterProperties;

	// Use this for initialization
	void Start ()
    {
        characterProperties = GameObject.Find("Character").GetComponent<CharacterProperties>();
	}

    void OnEnable() {
        characterProperties = GameObject.Find("Character").GetComponent<CharacterProperties>();

        if (characterProperties.female) 
        {
            SpriteState st = new SpriteState();
            st.disabledSprite = femaleIcon;
            st.highlightedSprite = femalePressed;
            st.pressedSprite = femalePressed;
            GetComponent<Button>().spriteState = st;

           GetComponent<Image>().sprite = femaleIcon;
        }
  
        else 
        {
            SpriteState st = new SpriteState();
            st.disabledSprite = maleIcon;
            st.highlightedSprite = malePressed;
            st.pressedSprite = malePressed;
            GetComponent<Button>().spriteState = st;

            GetComponent<Image>().sprite=maleIcon;
 
        }
    }

    public void ChangeBodyType()
    {
        characterProperties.bodyType = bodyType;
        if (characterProperties.female)
        {
            if (characterProperties.skinColor == 1)
            {
                characterProperties.unpushedBoobs = body.sprite = fPale;
            }
            else if (characterProperties.skinColor == 2)
            {
                characterProperties.unpushedBoobs = body.sprite = fBright;
            }
            else if (characterProperties.skinColor == 3)
            {
                characterProperties.unpushedBoobs = body.sprite = fTanned;
            }
            else if (characterProperties.skinColor == 4)
            {
                characterProperties.unpushedBoobs = body.sprite = fDark;
            }
        }
        else
        {
            if (characterProperties.skinColor == 1)
            {
                body.sprite = mPale;
            }
            else if (characterProperties.skinColor == 2)
            {
                body.sprite = mBright;
            }
            else if (characterProperties.skinColor == 3)
            {
                body.sprite = mTanned;
            }
            else if (characterProperties.skinColor == 4)
            {
                body.sprite = mDark;
            }
        }
        characterProperties.readyToProceed = true;
    }
}
