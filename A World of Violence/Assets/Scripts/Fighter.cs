using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour
{

    CharacterActions Brain;

    public GameObject Weapon;
    public GameObject Unarmed;

    GameObject CurrentAttack;

    bool PreviousAttack;

    public float MaxAttackDuration;
    float AttackDuration;
    float DrawbackDuration;

    public float ChargeDuration;

    public int ChargeDirection; //0= up, 1 = right, 2 = down, 3 = left;
    public int PreviousChargeDirection;

    bool IsBlocking;

    //LineCastPhysicsScript FighterPhysics;

    // Use this for initialization
    void Start()
    {
        
        Brain = GetComponent<CharacterActions>();
        
    }

    //For isdownonces and preparations for FixedUpdate;
    void PreUpdate()
    {

        
        if (Brain.upAttack || Brain.downAttack || Brain.leftAttack || Brain.rightAttack)
        {
            ChargeDuration += Time.deltaTime;
            PreviousAttack = true;
        }
        else
        {
            ChargeDuration = 0;
            PreviousAttack = false;
        }
        //Brain.UpdateCommands();
    }

    void Update()
    {
        PreUpdate();
        Combat();
    }

    

    void Combat()
    {
        Blocking();
        Attacking();
    }

    void Attacking()
    {
        if (AttackDuration > 0)
        {
            AttackDuration -= Time.deltaTime;
            if (AttackDuration <= 0)
            {
                CurrentAttack.SetActive(false);
                CurrentAttack = null;
            }

        }
        if (DrawbackDuration > 0)
        {
            DrawbackDuration -= Time.deltaTime;
        }
        else if(Brain.block)
        {
            ChargeDuration = 0;
        }
        else
        {
            GameObject tempwep;
            if (Weapon != null)
            {
                tempwep = Weapon;
            }
            else tempwep = Unarmed;

            if (Brain.upAttack)
            {
                if (!Brain.PreviousupAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 0;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().UpAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 0)
                {

                    if (PreviousChargeDirection == 1 && Brain.rightAttack)
                    {
                        ChargeDirection = 1;
                    }
                    else if (PreviousChargeDirection == 2 && Brain.downAttack)
                    {
                        ChargeDirection = 2;
                    }
                    else if (PreviousChargeDirection == 3 && Brain.leftAttack)
                    {
                        ChargeDirection = 3;
                    }
                    else
                    {
                        if (Brain.rightAttack)
                        {
                            ChargeDirection = 1;
                        }
                        else if (Brain.downAttack)
                        {
                            ChargeDirection = 2;
                        }
                        else if (Brain.leftAttack)
                        {
                            ChargeDirection = 3;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().UpAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().UpAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 0;
                }
            }

            if (Brain.downAttack)
            {
                if (!Brain.PreviousdownAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 2;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().DownAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 2)
                {

                    if (PreviousChargeDirection == 1 && Brain.rightAttack)
                    {
                        ChargeDirection = 1;
                    }
                    else if (PreviousChargeDirection == 0 && Brain.upAttack)
                    {
                        ChargeDirection = 0;
                    }
                    else if (PreviousChargeDirection == 3 && Brain.leftAttack)
                    {
                        ChargeDirection = 3;
                    }
                    else
                    {
                        if (Brain.rightAttack)
                        {
                            ChargeDirection = 1;
                        }
                        else if (Brain.upAttack)
                        {
                            ChargeDirection = 0;
                        }
                        else if (Brain.leftAttack)
                        {
                            ChargeDirection = 3;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().DownAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().DownAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 2;

                }
            }

            if (Brain.leftAttack)
            {
                if (!Brain.PreviousleftAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 3;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().BackAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 3)
                {

                    if (PreviousChargeDirection == 1 && Brain.rightAttack)
                    {
                        ChargeDirection = 1;
                    }
                    else if (PreviousChargeDirection == 0 && Brain.upAttack)
                    {
                        ChargeDirection = 0;
                    }
                    else if (PreviousChargeDirection == 2 && Brain.downAttack)
                    {
                        ChargeDirection = 2;
                    }
                    else
                    {
                        if (Brain.rightAttack)
                        {
                            ChargeDirection = 1;
                        }
                        else if (Brain.upAttack)
                        {
                            ChargeDirection = 0;
                        }
                        else if (Brain.downAttack)
                        {
                            ChargeDirection = 2;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().BackAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().BackAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }

                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 3;
                }
            }

            if (Brain.rightAttack)
            {
                if (!Brain.PreviousrightAttack)
                {
                    PreviousChargeDirection = ChargeDirection;
                    ChargeDirection = 1;
                }

                if (!PreviousAttack)
                {
                    CurrentAttack = tempwep.GetComponent<WeaponScript>().FrontAttack1;
                    CurrentAttack.SetActive(true);
                    AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                    DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                }
            }
            else
            {
                if (ChargeDirection == 1)
                {

                    if (PreviousChargeDirection == 3 && Brain.leftAttack)
                    {
                        ChargeDirection = 3;
                    }
                    else if (PreviousChargeDirection == 0 && Brain.upAttack)
                    {
                        ChargeDirection = 0;
                    }
                    else if (PreviousChargeDirection == 2 && Brain.downAttack)
                    {
                        ChargeDirection = 2;
                    }
                    else
                    {
                        if (Brain.leftAttack)
                        {
                            ChargeDirection = 3;
                        }
                        else if (Brain.upAttack)
                        {
                            ChargeDirection = 0;
                        }
                        else if (Brain.downAttack)
                        {
                            ChargeDirection = 2;
                        }
                        else
                        {
                            if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack3ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().FrontAttack3;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            else if (ChargeDuration > tempwep.GetComponent<WeaponScript>().Attack2ChargeThreshold)
                            {
                                CurrentAttack = tempwep.GetComponent<WeaponScript>().FrontAttack2;
                                CurrentAttack.SetActive(true);
                                AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
                                DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
                            }
                            ChargeDuration = 0;
                        }
                    }
                    PreviousChargeDirection = 1;
                }
            }
        }
    }

    void Blocking()
    {
        if (Brain.block)
        {
            if (DrawbackDuration < 0)
            {
                IsBlocking = true;
            }
        }
        else IsBlocking = false;
    }

}
