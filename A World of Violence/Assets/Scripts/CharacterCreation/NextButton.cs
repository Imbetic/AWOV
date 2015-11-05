using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour {

    public GameObject nextState;
    public bool canContinue = false;
    bool readytoproceed;

    void OnEnable()
    {
        readytoproceed = canContinue;
        GameObject.Find("Character").GetComponent<CharacterProperties>().readyToProceed = canContinue;
    }

    public void Next()
    {
        if(GameObject.Find("Character").GetComponent<CharacterProperties>().readyToProceed || readytoproceed)
        {
            nextState.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
