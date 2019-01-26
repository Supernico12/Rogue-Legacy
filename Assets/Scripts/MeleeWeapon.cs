using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapo : MonoBehaviour
{

    [Header("Pattern")]
    public float[] combatPattern;
    public AnimationClip[] animations;
    public float[] speedperAttack;
    [Header("AttackArea")]
    public float radious;
    public Vector3 offset;

    public override void Interact()
    {
        float direction = transform.localScale.x;
        Collider2D[] results = new Collider2D[10];
        Physics2D.OverlapBoxNonAlloc(stats.offset * direction + transform.position, stats.radious, 0, results);
        foreach (Collider2D result in results)
        {
            if (result != null && result != colliderplayer)
            {

                CharacterStats targetstats = result.GetComponent<CharacterStats>();
                if (targetstats != null)
                    targetstats.TakeDamage(weapon1.baseDamage * weapon1.combatPattern[(currentAttackCycle) % cycleLenght]);
                //Rigidbody2D rb = result.GetComponent<Rigidbody2D>();
                //if (rb != null)
                //rb.AddForce(new Vector2(stats.GetKnockBack.x * direction, stats.GetKnockBack.y), ForceMode2D.Impulse);

            }
        }
    }
