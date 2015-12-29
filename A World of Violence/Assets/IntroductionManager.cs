using UnityEngine;
using System.Collections;

public class IntroductionManager : MonoBehaviour {

    public GameObject customizationCanvas, introductionCanvas, BehindText;
    public AudioSource MusicManager;
    float timer;
    float previousTimer;

    bool firstframe = true;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

	void Update () 
    {

        if (timer >= 0.3 && previousTimer < 0.3)
        {
            MusicManager.enabled = true;
        }

        if (firstframe)
        {
            firstframe = false;
        }
        else
        {
            previousTimer = timer;
            timer += Time.deltaTime;
        }

        if (timer >= 1.7 && previousTimer < 1.7)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 0)
                {
                    BehindText.SetActive(true);
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                }
                
            }
        }
        
        if (timer >= 8.3 && previousTimer < 8.3)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 1)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i-1).gameObject.SetActive(false);
                }
                
            }
        }
        
        if (timer >= 14.7 && previousTimer < 14.7)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 2)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
               
            }
        }
        
        if (timer >= 21.2 && previousTimer < 21.2)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 3)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
               
            }
        }
        
        if (timer >= 27.5 && previousTimer < 27.5)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 4)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
               
            }
        }

        if (timer >= 27.5 && previousTimer < 27.5)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 4)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
               
            }
        }

        if (timer >= 33.95 && previousTimer < 33.95)
        {
            for(int i = 0; i< introductionCanvas.transform.childCount; i++)
            {
                if (i == 5)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
               
            }
        }

        if (timer >= 40.4 && previousTimer < 40.4)
        {
            for (int i = 0; i < introductionCanvas.transform.childCount; i++)
            {
                if (i == 6)
                {
                    introductionCanvas.transform.GetChild(i).gameObject.SetActive(true);
                    introductionCanvas.transform.GetChild(i - 1).gameObject.SetActive(false);
                }
               
            }
        }

        if(timer >= 46.7 && previousTimer < 46.7)
        {
            customizationCanvas.SetActive(true);
            introductionCanvas.transform.parent.gameObject.SetActive(false);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
