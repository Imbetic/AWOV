using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{

    public Transform player1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, player1.position, Time.deltaTime*4);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        
    }
}
