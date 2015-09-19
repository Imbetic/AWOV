using UnityEngine;
using System.Collections;

public class PlayerController : CharacterActions
{

    public override void UpdateCommands()
    {
        base.UpdateCommands();

        if (Input.GetKey(KeyCode.W))
        {
            moveUp = true;
        }
        else moveUp = false;

        if (Input.GetKey(KeyCode.S))
        {
            moveDown = true;
        }
        else moveDown = false;

        if (Input.GetKey(KeyCode.A))
        {
            moveLeft = true;
        }
        else moveLeft = false;

        if (Input.GetKey(KeyCode.D))
        {
            moveRight = true;
        }
        else moveRight = false;

        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        else jump = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            upAttack = true;
        }
        else upAttack = false;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            downAttack = true;
        }
        else downAttack = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftAttack = true;
        }
        else leftAttack = false;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightAttack = true;
        }
        else rightAttack = false;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            block = true;
        }
        else block = false;

    }

}
