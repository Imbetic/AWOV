using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

    public GameObject previousState;

    public bool resetProperties;

    public GameObject CanvasPrefab;

	public void Back()
    {
        if (resetProperties)
        {
            GameObject.Find("Character").GetComponent<CharacterProperties>().ResetProperties(true);
        }
        previousState.SetActive(true);   
        transform.parent.gameObject.SetActive(false);
    }

    public void ResetAll()
    {

    }
}
