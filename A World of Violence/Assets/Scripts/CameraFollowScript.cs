using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{

    public Transform player1;
    BoxCollider2D theBox;

    bool moveX = true;
    bool moveY = true;

    // Use this for initialization
    void Start()
    {
        theBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float xpos = transform.position.x;
        float ypos = transform.position.y;


        xpos = Mathf.Lerp(transform.position.x, player1.position.x, Time.deltaTime * 4);
        if (xpos < (-14.5f + theBox.size.x / 2)) xpos = (-14.5f + theBox.size.x / 2);
        else if (xpos > (14.5f - theBox.size.x / 2)) xpos = (14.5f - theBox.size.x / 2);

        ypos = Mathf.Lerp(transform.position.y, player1.position.y, Time.deltaTime * 4);
        if (ypos < -8 + (theBox.size.y / 2)) ypos = -8 + (theBox.size.y / 2);
        else if (ypos > 8 - (theBox.size.y / 2)) ypos = 8 - (theBox.size.y / 2);

        transform.position = new Vector3(xpos, ypos, -10);

        theBox.size = new Vector2(((float)Screen.width / (float)Screen.height) * 10, 10);

    }

}
