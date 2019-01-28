using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Animator animator;
    CharacterStats stats;
    [SerializeField] float detectionRadious;
    [SerializeField] float attackRadious;
    [SerializeField] float speed;
    [SerializeField] float stopDistance = 1;
    [SerializeField] LayerMask layerMask;
    public float recoveryTime = .25f;

    Collider2D myColider;
    float lastAttack;

    bool isAttacking;
    Transform target;
    bool isAlerted;


    public void SetAlerted(bool state)
    {
        isAlerted = state;
    }
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        myColider = gameObject.GetComponent<Collider2D>();
    }


    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);


        animator.SetBool("isMoving", false);
        if (distance < detectionRadious || isAlerted)
        {

            FaceTarget();
            if (distance > stopDistance)
            {
                Chase();
            }

            if (distance < attackRadious)
            {
                Attack();
            }
        }

        lastAttack -= Time.deltaTime;



    }

    void Chase()
    {
        if (!isAttacking)
        {
            float horizontalMovement = (target.position - transform.position).normalized.x;
            transform.Translate(new Vector2(horizontalMovement, 0) * Time.deltaTime * speed);
            animator.SetBool("isMoving", true);
        }
    }
    void FaceTarget()
    {
        if (!isAttacking)
        {
            if (transform.position.x - target.position.x < 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }
    void Attack()
    {
        if (lastAttack <= 0)
        {
            animator.SetTrigger("attack");
            lastAttack = stats.GetAttackSpeed;
            isAttacking = true;
        }

    }

    public void OnAttackEvent()
    {
        float direction = transform.localScale.x;
        Collider2D[] results = new Collider2D[10];

        Physics2D.OverlapBoxNonAlloc(stats.offset * direction + transform.position, stats.radious, 0, results, layerMask);
        foreach (Collider2D result in results)
        {

            if (result != null && result != myColider)
            {
                Debug.Log(result.name);
                CharacterStats targetstats = result.GetComponent<CharacterStats>();
                if (targetstats != null)
                    targetstats.TakeDamage(stats.GetDamage);

            }

        }
        isAttacking = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadious);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadious);
    }
}
