using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextButton : MonoBehaviour {

    public GameObject nextState;
    public bool canContinue = false;
    public Button ownedbutton;

    void OnEnable()
    {
        if(!canContinue)
        {
            ownedbutton.enabled = false;
        }
        GameObject.Find("Character").GetComponent<CharacterProperties>().readyToProceed = canContinue;
    }

    public void Next()
    {
        //if(GameObject.Find("Character").GetComponent<CharacterProperties>().readyToProceed || readytoproceed)
        //{
            nextState.SetActive(true);
            transform.parent.gameObject.SetActive(false);
       // }
    }
}
