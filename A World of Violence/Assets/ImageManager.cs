using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {

    public Sprite pressed, idle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void OnPressed()
    {
        for(int i = 0; i < transform.parent.childCount; i++)
        {
            if(transform.parent.GetChild(i) != transform)
            {
                transform.parent.GetChild(i).gameObject.GetComponent<Image>().sprite = transform.parent.GetChild(i).gameObject.GetComponent<ImageManager>().idle;
                if(transform.parent.GetChild(i).GetChild(0) != null)
                {
                    transform.parent.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                GetComponent<Image>().sprite = pressed;
                if(transform.GetChild(0) != null)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

}
