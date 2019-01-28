using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerFighting : MonoBehaviour
{



    [SerializeField]
    float combatTime = 5;
    [SerializeField]
    Weapon weapon1;
    [SerializeField]
    Weapon weapon2;






    int cycleLenght = 3;
    CharacterStats stats;
    CapsuleCollider2D colliderplayer;
    PlayerMotor playerMotor;
    float combatTimer;
    float lastAttack = -10;

    public bool isOnCombat { get; private set; }
    Animator animator;

    [Header("Animations")]

    [SerializeField] AnimationClip replaceableMove;
    [SerializeField] AnimationClip replasceableIdle;

    [SerializeField] AnimationClip[] playerAnimationsMove;
    [SerializeField] AnimationClip[] playerAnimationsIdle;
    AnimatorOverrideController overrideAnimator;

    MeleeWeaponController meleeWeapon;
    RangedWeaponController rangedWeapon;

    void Start()
    {

        animator = GetComponentInChildren<Animator>();

        stats = GetComponent<CharacterStats>();
        playerMotor = GetComponent<PlayerMotor>();
        colliderplayer = GetComponent<CapsuleCollider2D>();
        meleeWeapon = GetComponent<MeleeWeaponController>();
        rangedWeapon = GetComponent<RangedWeaponController>();
        //currentAttackCycle = weapon1.combatPattern.Length;
        //cycleLenght = currentAttackCycle;
    }
    void AttackInput()
    {
        if (combatTimer <= 0)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack(weapon1);

            }
            if (Input.GetButtonDown("Attack2"))
            {
                Attack(weapon2);
            }


        }
    }

    void Attack(Weapon weapon)
    {
        if (weapon.type == WeaponType.Sword)
        {
            combatTimer = meleeWeapon.Attack(weapon);
        }
        else if (weapon.type == WeaponType.Bow)
        {
            combatTimer = rangedWeapon.BowAttack(weapon);
        }

        lastAttack = Time.time;
        playerMotor.SetCanMove(false);
    }
    void Update()
    {

        // BowAttack();
        AttackInput();

        combatTimer -= Time.deltaTime;


        if (Time.time - lastAttack < combatTime)
        {
            isOnCombat = true;
        }
        else
        {
            isOnCombat = false;
        }

        /*
    if (isOnCombat)
    {
        overrideAnimator[replaceableMove.name] = playerAnimationsMove[1];
        overrideAnimator[replasceableIdle.name] = playerAnimationsIdle[1];
    }
    else
    {

        overrideAnimator[replaceableMove.name] = playerAnimationsMove[0];
        overrideAnimator[replasceableIdle.name] = playerAnimationsIdle[0];
    }
        */
    }


    void OnDrawGizmos()
    {
        if (weapon1 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + weapon1.offset, weapon1.radious);
        }

        if (weapon2 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + weapon2.offset, weapon2.radious);
        }
    }
}
