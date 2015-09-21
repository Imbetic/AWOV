using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour
{

    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;

    public bool jump = false;

    public bool upAttack = false;
    public bool downAttack = false;
    public bool leftAttack = false;
    public bool rightAttack = false;

    public bool PreviousupAttack = false;
    public bool PreviousdownAttack = false;
    public bool PreviousleftAttack = false;
    public bool PreviousrightAttack = false;

    public bool block = false;

    public bool special1;
    public bool special2;
    public bool special3;

    public bool dropItem;
    public bool pickItem;

    public virtual void UpdateCombatCommands()
    {
        PreviousupAttack = upAttack;
        PreviousdownAttack = downAttack;
        PreviousleftAttack = leftAttack;
        PreviousrightAttack = rightAttack;
    }

    public virtual void UpdateMovementCommands()
    {
        
    }

}
