using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Reseter : MonoBehaviour
{

    public Transform buttons;

    public void ResetAll()
    {
        RecursiveChildrenDisabling(buttons);
    }

    void RecursiveChildrenDisabling(Transform oc)
    {
        if (oc.childCount == 1)
        {
            if (oc.GetChild(0).childCount == 0)
            {
                if (oc.GetComponent<ImageManager>() != null && oc.GetComponent<Image>() != null)
                    oc.GetComponent<Image>().sprite = oc.GetComponent<ImageManager>().idle;
                if (oc.GetComponent<CharacterPartButton>() != null)
                {
                    if (oc.GetComponent<CharacterPartButton>().Colors != null)
                        oc.GetComponent<CharacterPartButton>().Colors.SetActive(false);
                }
                oc.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                RecursiveChildrenDisabling(oc.GetChild(0));
            }
        }
        else
        {
            for (int i = 0; i < oc.childCount; i++)
            {
                RecursiveChildrenDisabling(oc.GetChild(i));
            }
        }
    }
}
