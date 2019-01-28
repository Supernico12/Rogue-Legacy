using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{

    RangedWeapon lastWeapon;
    PlayerMotor playerMotor;
    Animator animator;
    PlayerAnimatorController playerAnimatorController;

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
    }
    public float BowAttack(Weapon weapon)
    {
        lastWeapon = weapon as RangedWeapon;


        // ChangeAttack Animation

        playerAnimatorController.OverrideAttack(weapon.animations[0]);
        animator.SetTrigger("attack");


        return weapon.baseSpeed;


    }


    public void FinishAttackingBow()
    {
        playerMotor.SetCanMove(true);
        float direction = transform.localScale.x;
        ArrowScript script = Instantiate(lastWeapon.arrowPrefab, lastWeapon.arrowSpawnOffset + transform.position, Quaternion.identity).GetComponent<ArrowScript>();
        //rb.AddForce(new Vector2(ArrowForce * direction, 0), ForceMode2D.Impulse);
        script.SetDamage(lastWeapon.baseDamage);
        script.transform.localScale *= transform.localScale.x;
        script.SetSpeed(lastWeapon.arrowForce * direction);
        // Bow Attack 
    }

}
