using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerFighting : MonoBehaviour
{



    [SerializeField]
    float combatTime = 5;
    [SerializeField] GameObject pickableWeapon;
    [SerializeField] float unequipForce = 10f;

    public Weapon[] weapons;
    public Weapon weapon1
    {
        get { return weapons[0]; }
        set { weapons[0] = value; }
    }
    public Weapon weapon2
    {
        get { return weapons[1]; }
        set { weapons[1] = value; }
    }






    int cycleLenght = 3;
    CharacterStats stats;
    CapsuleCollider2D colliderplayer;
    PlayerMotor playerMotor;
    float combatTimer;
    float lastAttack = -10;


    public bool isOnCombat { get; private set; }
    Animator animator;
    public event System.Action OnWeaponChange;



    [Header("Animations")]

    [SerializeField] AnimationClip[] playerAnimationsMove;
    [SerializeField] AnimationClip[] playerAnimationsIdle;

    PlayerAnimatorController playerAnimatorController;

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
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
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


        if (isOnCombat)
        {
            playerAnimatorController.OverrideMove(playerAnimationsMove[1]);
            playerAnimatorController.OverrideIdle(playerAnimationsIdle[1]);

        }
        else
        {

            playerAnimatorController.OverrideMove(playerAnimationsMove[0]);
            playerAnimatorController.OverrideIdle(playerAnimationsIdle[0]);

        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeWeapons();
        }

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

    public void EquipWeapon(Weapon weapon, int index)
    {

        if (weapons[index] != null)
        {
            UnequipWeapon(index);
        }
        weapons[index] = weapon;
        OnWeaponChange.Invoke();
    }

    public void ChangeWeapons()
    {
        Weapon weapon = weapon2;
        weapon2 = weapon1;
        weapon1 = weapon;
        OnWeaponChange.Invoke();
    }

    public void UnequipWeapon(int index)
    {
        GameObject pickWeapon = Instantiate(pickableWeapon, transform.position, Quaternion.identity);

        pickWeapon.GetComponent<PickableWeapon>().weapon = weapons[index];
        pickWeapon.GetComponent<SpriteRenderer>().sprite = weapons[index].sprite;
        Rigidbody2D rb = pickWeapon.GetComponent<Rigidbody2D>();

        rb.AddForce(Interactable.ThrowForce(), ForceMode2D.Impulse);
        OnWeaponChange.Invoke();
    }
}
