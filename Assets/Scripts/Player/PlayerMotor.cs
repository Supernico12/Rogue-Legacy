using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMotor : MonoBehaviour
{


    CapsuleCollider2D colliderplayer;
    PlayerFighting playerCombat;
    CharacterStats stats;

    [SerializeField]
    float movementSpeed = 1;
    [SerializeField]
    float gravity = 10;
    [SerializeField]
    float jumpStrenght = 8;
    [SerializeField] BackgroundScrolling scrolling;

    [SerializeField] float rollCooldown;

    int layerMask;
    ContactFilter2D filter;






    bool canMove = true;
    bool isGrounded;
    bool isRoling;
    Animator animator;

    Vector2 movement = Vector2.zero;
    float lastRoll;
    public void SetCanMove(bool state)
    {
        canMove = state;
    }

    // Use this for initialization
    void Start()
    {
        colliderplayer = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerFighting>();
        stats = GetComponent<CharacterStats>();
        movementSpeed = stats.GetMovementSpeed;


        filter.useTriggers = false;
        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        filter.useLayerMask = true;


    }



    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");


        float movementHorizontal = x;




        if (!isRoling)
        {
            if (!canMove)
            {
                movement.x = movementHorizontal / 3;
            }
            else
            {
                movement.x = movementHorizontal;
            }
        }



        if (movementHorizontal != 0)
        {
            if (!isRoling)
                transform.localScale = new Vector3(movementHorizontal, 1, 1);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }


    }

    void Roll()
    {
        if (Input.GetButtonDown("Roll"))
        {
            if (lastRoll <= 0)
            {
                if (isGrounded)
                {

                    //Roll
                    isRoling = true;
                    animator.SetBool("isRoll", true);
                    stats.SetTargeatable(false);
                    movementSpeed *= 2;
                    movement.x = transform.localScale.x;
                    lastRoll = rollCooldown;

                }
            }
        }
    }

    public void OnFinishRoll()
    {
        animator.SetBool("isRoll", false);
        stats.SetTargeatable(true);
        movementSpeed /= 2;
        isRoling = false;
    }
    void IsGrounded()
    {
        // isGrounded =Physics.CheckCapsule(colliderplayer.bounds.center, new Vector3(colliderplayer.bounds.center.x, colliderplayer.bounds.min.y - 0.1f, colliderplayer.bounds.center.z), 0.9f);

        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        float radius = 0.25f;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = transform.position - new Vector3(0, 0.6f, 0);

        //returns true if the sphere touches something on that layer
        Collider2D[] results = new Collider2D[16];
        //int cantOBJ = Physics2D.OverlapCircleNonAlloc(pos, radius, results,layerMask);
        int cantOBJ = Physics2D.OverlapCircle(pos, radius, filter, results);
        if (cantOBJ > 0)
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;

        }
        /*
		foreach (Collider2D result in results)
		{
			if (result != null)
			{
				Debug.Log(result.name);
			}
		}
		*/
    }


    // Update is called once per frame
    void FixedUpdate()
    {


        IsGrounded();
        Roll();
        Move();



        lastRoll -= Time.deltaTime;
        if (isGrounded)
        {

            animator.SetBool("isJumping", false);
            movement.y = 0;
            if (Input.GetButton("Jump"))
            {
                movement.y = jumpStrenght;
                animator.SetBool("isJumping", true);

            }



        }
        else
        {
            movement.y -= gravity * Time.deltaTime;
        }

        Vector2 finalMovement = new Vector2(movement.x * movementSpeed, movement.y);

        transform.Translate(finalMovement * Time.deltaTime);


        scrolling.SetDirection(finalMovement.x);
        //controller.Move(finalMovement  * Time.deltaTime);
    }

}
