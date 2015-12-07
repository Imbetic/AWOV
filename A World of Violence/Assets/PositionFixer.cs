using UnityEngine;
using System.Collections;

public class PositionFixer : MonoBehaviour {

    public RectTransform FixPos;

	// Use this for initialization
	void Start () 
    {
        GetComponent<RectTransform>().position= FixPos.position;
        Debug.Log(GetComponent<RectTransform>().position + " " + FixPos.position);
	}
	 void OnEnable()
    {
        GetComponent<RectTransform>().position = FixPos.position;
    }
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().position = FixPos.position;
	}
}
