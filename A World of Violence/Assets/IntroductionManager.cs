using UnityEngine;
using System.Collections;

public class IntroductionManager : MonoBehaviour {

    public GameObject customizationCanvas, introductionCanvas;
    float timer;
    float previousTimer;

	void Update () 
    {
        previousTimer = timer;
        timer += Time.deltaTime;
        if (timer >= 2 && previousTimer < 2)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 0)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        if (timer >= 8.5 && previousTimer < 8.5)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 1)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        if (timer >= 14.5 && previousTimer < 14.5)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 2)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        if (timer >= 21 && previousTimer < 21)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 3)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        if (timer >= 27.5 && previousTimer < 27.5)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 4)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (timer >= 27.5 && previousTimer < 27.5)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 4)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (timer >= 34 && previousTimer < 34)
        {
            for(int i = 0; i< introductionCanvas.transform.childCount; i++)
            {
                if (i == 5)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (timer >= 40 && previousTimer < 40)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 6)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                else introductionCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if(timer >= 46.5 && previousTimer < 46.5)
        {
            customizationCanvas.SetActive(true);
            introductionCanvas.SetActive(false);
        }
	}
}
