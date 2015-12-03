using UnityEngine;
using System.Collections;

public class SkipButton : MonoBehaviour {

    public GameObject nextCanvas;
    public GameObject thisCanvas;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Skip () {
        thisCanvas.SetActive(false);
        nextCanvas.SetActive(true);
	}
}
