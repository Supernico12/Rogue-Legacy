using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{


    [SerializeField] LayerMask layerMask;

    Animator animator;
    PlayerAnimatorController playerAnimatorController;

    [SerializeField] Weapon lastWeapon;
    int currentAttackCycle = 3;
    int cycleLenght
    {
        get { return lastWeapon.combatPattern.Length; }
    }
    PlayerMotor playerMotor;
    [SerializeField]
    float combatCycleReset;

    float lastCombatCycle;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMotor = GetComponent<PlayerMotor>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
    }
    public void FinishedAttack()
    {
        float direction = transform.localScale.x;
        Collider2D[] results = new Collider2D[10];
        Physics2D.OverlapBoxNonAlloc(lastWeapon.offset * direction + transform.position, lastWeapon.radious, 0, results, layerMask);
        foreach (Collider2D result in results)
        {
            if (result != null && result)
            {

                CharacterStats targetstats = result.GetComponent<CharacterStats>();
                if (targetstats != null)
                    targetstats.TakeDamage(lastWeapon.baseDamage * lastWeapon.combatPattern[(currentAttackCycle) % cycleLenght]);
                //Rigidbody2D rb = result.GetComponent<Rigidbody2D>();
                //if (rb != null)
                //rb.AddForce(new Vector2(stats.GetKnockBack.x * direction, stats.GetKnockBack.y), ForceMode2D.Impulse);

            }
        }
        currentAttackCycle++;
        playerMotor.SetCanMove(true);

    }

    void Update()
    {
        lastCombatCycle -= Time.deltaTime;
        if (lastCombatCycle < 0)
        {
            currentAttackCycle = cycleLenght;
        }
    }

    public float Attack(Weapon weapon)
    {
        lastWeapon = weapon;

        Debug.Log(weapon.animations[(currentAttackCycle) % cycleLenght]);
        playerAnimatorController.OverrideAttack(weapon.animations[(currentAttackCycle) % cycleLenght]);
        animator.SetFloat("attackSpeed", 1 / weapon.combatPattern[currentAttackCycle % cycleLenght]);
        animator.SetTrigger("attack");

        //Debug.Log(combatTimer.ToString());
        //playerMotor.SetCanMove(false);

        lastCombatCycle = combatCycleReset;
        return weapon.combatPattern[(currentAttackCycle) % cycleLenght];

    }

}


