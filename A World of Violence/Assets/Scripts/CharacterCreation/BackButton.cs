using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

    public GameObject previousState;

    public bool resetProperties;


	public void Back()
    {
        previousState.SetActive(true);
        if(resetProperties)
        {
            GameObject.Find("Character").GetComponent<CharacterProperties>().ResetProperties(true);
        }
        transform.parent.gameObject.SetActive(false);
    }
}
