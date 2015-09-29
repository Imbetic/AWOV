using UnityEngine;
using System.Collections;

public class ParallaxScript : MonoBehaviour
{

    Transform PointOfView;

    Transform TheParent;

    float DistFromCamera = -1;

    void Start()
    {
        PointOfView = GameObject.Find("Main Camera").transform;
        TheParent = transform.parent;

        DistFromCamera = TheParent.position.z - PointOfView.position.z;

        if (DistFromCamera > 0)
        {
            transform.position = new Vector3((TheParent.position.x / (DistFromCamera / 10)) + (PointOfView.position.x * (1 - (1 / (DistFromCamera / 10)))), (TheParent.position.y / (DistFromCamera / 10)) + (PointOfView.position.y * (1 - (1 / (DistFromCamera / 10)))), TheParent.position.z);
        }

    }

    void Update()
    {
        DistFromCamera = TheParent.position.z - PointOfView.position.z;
        
        if (DistFromCamera > 0)
        {
            transform.position = new Vector3((TheParent.position.x / (DistFromCamera / 10)) + (PointOfView.position.x * (1 - (1 / (DistFromCamera / 10)))), (TheParent.position.y / (DistFromCamera / 10)) + (PointOfView.position.y * (1 - (1 / (DistFromCamera / 10)))), TheParent.position.z);
        }
    }
}
