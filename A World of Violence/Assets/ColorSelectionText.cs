using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorSelectionText : MonoBehaviour {

    public Image colorSelection;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void OnEnable()
    {
        colorSelection.enabled = true;
    }

    void OnDisable()
    {
        colorSelection.enabled = false;
    }
}
