using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VE_Button : MonoBehaviour {
    public bool isTargeted; //set to true on starting button, false on the rest
    public bool isTargetedThisFrame;
    public VE_Button rightButton, leftButton, downButton, upButton; //These are the buttons you can switch to from the current one. Assign them in the editor.
    public KeyCode rightKey = KeyCode.RightArrow, leftKey = KeyCode.LeftArrow, downKey = KeyCode.DownArrow, upKey = KeyCode.UpArrow, enterKey = KeyCode.KeypadEnter; //Default is arrow keys and Enter button. Bind keys to these in editor.
    public Sprite idleImage, targetedImage, pressedImage; //What the button looks like during these stages. Assign an image to each stage in the editor.

    VE_Button nextButton;

	// Use this for initialization
	void Start () {
        if (isTargeted)
        {
            GetComponent<Image>().sprite = targetedImage;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(isTargetedThisFrame)
        {
            isTargetedThisFrame = false;
        }
        else if (isTargeted)
        {
            if (Input.GetKeyDown(rightKey))
            {
                if (rightButton != null)
                {
                    nextButton = rightButton;
                }
            }
            if (Input.GetKeyDown(leftKey))
            {
                if (leftButton != null)
                {
                    nextButton = leftButton;
                }
            }
            if (Input.GetKeyDown(downKey))
            {
                if (downButton != null)
                {
                    nextButton = downButton;
                }
            }
            if (Input.GetKeyDown(upKey))
            {
                if (upButton != null)
                {
                    nextButton = upButton;
                }
            }

            if (nextButton != null)
            {
                GetComponent<Image>().sprite = idleImage;
                isTargeted = false;

                nextButton.GetComponent<Image>().sprite = nextButton.targetedImage;
                nextButton.isTargeted = true;
                nextButton.isTargetedThisFrame = true;
                nextButton = null;

            }

            if (Input.GetKeyDown(enterKey))
            {

            }
        }


	}
}
