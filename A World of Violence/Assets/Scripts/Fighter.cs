using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour
{

    CharacterActions Brain;
    RaycastPlatformer Body;

    public GameObject Weapon;
    WeaponScript WeaponPropoerties;
    public GameObject Unarmed;

    public SpriteRenderer WeaponVisuals;
    //public SpriteRenderer TorsoVisuals;

    public GameObject TheTorso;

    public GameObject CurrentAttack;

    bool PreviousAttack;

    public bool isAttacking = false;

    public float MaxAttackDuration;
    float AttackDuration;
    float DrawbackDuration;
    float AttackDelayTimer;

    public float ChargeDuration;

    public int ChargeDirection; //0= up, 1 = right, 2 = down, 3 = left;
    public int PreviousChargeDirection;

    bool IsBlocking;

    //LineCastPhysicsScript FighterPhysics;

    // Use this for initialization
    void Start()
    {
        Body = GetComponent<RaycastPlatformer>();
        Brain = GetComponent<CharacterActions>();
        Brain.UpdateCombatCommands();

        CurrentAttack = Weapon.GetComponent<WeaponScript>().FrontAttack1;
        WeaponPropoerties = Weapon.GetComponent<WeaponScript>();
        WeaponVisuals.sprite = WeaponPropoerties.IdleRightSprite;
    }

    //For isdownonces and preparations for FixedUpdate;
    void PreUpdate()
    {
        Charging();
        Brain.UpdateCombatCommands();
    }

    void Charging()
    {
        if (Brain.upAttack || Brain.downAttack || Brain.leftAttack || Brain.rightAttack)
        {
            ChargeDuration += Time.deltaTime;
            //if (!isAttacking)
            //{
            //    if (Brain.leftAttack || Brain.rightAttack)
            //    {
            //        TheTorso.GetComponent<Animator>().SetBool("CS1", true);
            //        //TorsoVisuals.sprite = TheTorso.SpearTHCharge1;
            //    }
                
            //}
            PreviousAttack = true;
        }
        else
        {
            ChargeDuration = 0;
            PreviousAttack = false;
        }
    }

    public void Fighting()
    {
        PreUpdate();
        Combat();
        if (!Brain.leftAttack && !Brain.rightAttack && !isAttacking)
        {
            TheTorso.GetComponent<Animator>().SetBool("CS1", false);
        }
        else
        {
            TheTorso.GetComponent<Animator>().SetBool("CS1", true);
        }
    }



    void Combat()
    {
        Blocking();
        Attacking();
    }

    void AttackStarted()
    {
        AttackDelayTimer -= Time.deltaTime;
        if (AttackDelayTimer <= 0)
        {
            CurrentAttack.SetActive(true);
            //TheTorso.GetComponent<Animator>().SetBool("CS1", false);
            TheTorso.GetComponent<Animator>().SetBool("AS1", true);
            CurrentAttack.GetComponent<BoxCollider2D>().enabled = true;
            if (CurrentAttack.GetComponent<AttackScript>().Direction == 1 || CurrentAttack.GetComponent<AttackScript>().Direction == 3)
            {
                //TorsoVisuals.sprite = TheTorso.SpearTHAttack1;
            }            
            AttackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDuration;
            DrawbackDuration = CurrentAttack.GetComponent<AttackScript>().AttackDrawBack;
        }
    }
    void AttackOngoing()
    {
        AttackDuration -= Time.deltaTime;
        if (AttackDuration <= 0)
        {
            CurrentAttack.SetActive(false);
        }
    }

    void CommenceAttack(int strength /* 1-3 */, int direction /* 0, 1, 2, 3*/)
    {
        isAttacking = true;

        if (direction == 0)
        {
            if (strength == 1)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().UpAttack1;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
            else if (strength == 2)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().UpAttack3;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
            else if (strength == 3)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().UpAttack2;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
        }
        else if (direction == 1)
        {
            if (strength == 1)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().FrontAttack1;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;

                    //TorsoVisuals.sprite = TheTorso.SpearTHCharge1;
                TheTorso.GetComponent<Animator>().SetBool("CS1", true);
                
            }
            else if (strength == 2)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().FrontAttack2;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
            else if (strength == 3)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().FrontAttack3;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
        }
        else if (direction == 2)
        {
            if (strength == 1)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().DownAttack1;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
            else if (strength == 2)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().DownAttack2;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
            else if (strength == 3)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().DownAttack3;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
        }
        else if (direction == 3)
        {
            if (strength == 1)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().BackAttack1;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;

                TheTorso.GetComponent<Animator>().SetBool("CS1", true);
                    //TorsoVisuals.sprite = TheTorso.SpearTHCharge1;
                
            }
            else if (strength == 2)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().BackAttack2;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
            else if (strength == 3)
            {
                CurrentAttack = Weapon.GetComponent<WeaponScript>().BackAttack3;
                AttackDelayTimer = CurrentAttack.GetComponent<AttackScript>().AttackDelay;
            }
        }
    }


    bool ChargeChecksUp()
    {
        if (Brain.upAttack)
        {

            if (!Brain.PreviousupAttack)
            {
                PreviousChargeDirection = ChargeDirection;
                ChargeDirection = 0;
            }

            if (!PreviousAttack)
            {
                CommenceAttack(1, 0);
            }
        }
        else
        {
            if (ChargeDuration <= 0)
            {
                WeaponVisuals.sprite = WeaponPropoerties.IdleRightSprite;
            }

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
                        return true;
                    }
                }
                PreviousChargeDirection = 0;
            }
        }

        return false;
    }

    bool ChargeChecksRight()
    {
        if (Brain.rightAttack)
        {
            if (!Brain.PreviousrightAttack)
            {
                PreviousChargeDirection = ChargeDirection;
                ChargeDirection = 1;
            }

            if (!PreviousAttack && !Brain.downAttack && !Brain.upAttack && !Brain.leftAttack)
            {
                CommenceAttack(1, 1);
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
                        return true;
                    }
                }
                PreviousChargeDirection = 1;
            }
        }
        return false;
    }

    bool ChargeChecksDown()
    {
        if (Brain.downAttack)
        {
            if (!Brain.PreviousdownAttack)
            {
                PreviousChargeDirection = ChargeDirection;
                ChargeDirection = 2;
            }

            if (!PreviousAttack && !Brain.upAttack)
            {
                CommenceAttack(1, 2);
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
                        return true;
                    }
                }
                PreviousChargeDirection = 2;

            }
        }
        return false;
    }

    bool ChargeChecksLeft()
    {
        if (Brain.leftAttack)
        {
            if (!Brain.PreviousleftAttack)
            {
                PreviousChargeDirection = ChargeDirection;
                ChargeDirection = 3;
            }

            if (!PreviousAttack && !Brain.downAttack && !Brain.upAttack)
            {
                CommenceAttack(1, 3);
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
                        return true;
                    }
                }
                PreviousChargeDirection = 3;
            }
        }
        return false;
    }

    void Attacking()
    {
        if (AttackDelayTimer > 0)
        {
            AttackStarted();
        }
        else
        {
            if (AttackDuration > 0)
            {
                AttackOngoing();
            }
            else
            {

                if (DrawbackDuration > 0)
                {
                    DrawbackDuration -= Time.deltaTime;      
                    if(DrawbackDuration < 0)
                    {
                        isAttacking = false;
                        
                        TheTorso.GetComponent<Animator>().SetBool("AS1", false);
                        
                    }
                }
                else if (Brain.block)
                {
                    ChargeDuration = 0;
                }
                else
                {
                    
                    
                    AttackChecks();
                }
            }
        }
    }

    void AttackChecks()
    {
        if (ChargeChecksUp())
        {
            if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack3ChargeThreshold)
            {
                CommenceAttack(2, 0);
            }
            else if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack2ChargeThreshold)
            {
                CommenceAttack(3, 0);
            }
            ChargeDuration = 0;
        }

        if (ChargeChecksRight())
        {
            if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack3ChargeThreshold)
            {
                CommenceAttack(3, 1);
            }
            else if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack2ChargeThreshold)
            {
                CommenceAttack(2, 1);
            }
            ChargeDuration = 0;
        }


        if (ChargeChecksDown())
        {
            if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack3ChargeThreshold)
            {
                CommenceAttack(3, 2);

            }
            else if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack2ChargeThreshold)
            {
                CommenceAttack(2, 2);

            }
            ChargeDuration = 0;
        }

        if (ChargeChecksLeft())
        {
            if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack3ChargeThreshold)
            {
                CommenceAttack(3, 3);

            }
            else if (ChargeDuration > Weapon.GetComponent<WeaponScript>().Attack2ChargeThreshold)
            {
                CommenceAttack(2, 3);

            }

            ChargeDuration = 0;
        }


        
    }

    void Blocking()
    {
        if (Brain.block)
        {
            if (DrawbackDuration <= 0)
            {
                IsBlocking = true;
            }
        }
        else IsBlocking = false;
    }

}
