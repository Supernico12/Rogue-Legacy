using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMotor : MonoBehaviour {


	CharacterController controller;
	CharacterStats stats;
	[SerializeField]
	float movementSpeed = 1;
	[SerializeField]
	float gravity = 10;
	[SerializeField]
	float jumpStrenght = 8;
	
	

	[SerializeField]
	Collider2D attackArea;

	float lastAttack;
	bool canMove = true;
	Animator animator;
	float attackSpeed;
	Vector2 movement = Vector2.zero;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		stats = GetComponent<CharacterStats>();
		attackSpeed = stats.GetAtqueSpeed;
	}


	public void FinishedAttacking()
	{
		canMove = true;
		/*
		Collider2D[] inRange;
		ContactFilter2D contactFilter = new ContactFilter2D();
		attackArea.OverlapCollider(contactFilter, inRange);
		*/
		float direction = transform.localScale.x; 
		Collider2D[] results = new Collider2D[10];
		Physics2D.OverlapBoxNonAlloc(stats.offset * direction + transform.position, stats.radious, 0, results);
		foreach( Collider2D result in results)
		{
			if (result != null)
			{
				CharacterStats targetstats = result.GetComponent<CharacterStats>();
				if (targetstats != null)
					targetstats.TakeDamage(stats.GetDamage);
				Rigidbody2D rb = result.GetComponent<Rigidbody2D>();
				if (rb != null)
					rb.AddForce(new Vector2(stats.GetKnockBack.x *direction , stats.GetKnockBack.y ),ForceMode2D.Impulse);
			}
			
		}
	}

	void Move()
	{
		float x = Input.GetAxisRaw("Horizontal");
		

		float  movementHorizontal = x;
		if (!canMove)
		{
			movementHorizontal = 0;
		}



			movement.x = movementHorizontal;


		if (movementHorizontal != 0)
		{
			transform.localScale = new Vector3(movementHorizontal, 1, 1);
			animator.SetBool("isMoving", true);
		}
		else
		{
			animator.SetBool("isMoving", false);
		}

		
	}
	void Attack()
	{
		if (Input.GetButton("Attack"))
		{
			if (lastAttack <= 0)
			{
				animator.SetTrigger("attack");
				lastAttack =  attackSpeed;
				canMove = false;
			}
			
		}

	}
	void Update()
	{
		lastAttack -= Time.deltaTime;
	}
	// Update is called once per frame
	void FixedUpdate () {

		Move();
		Attack();
		
		if (controller.isGrounded)
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
		
		Vector2 finalMovement = new Vector2(movement.x * movementSpeed, movement.y );
		controller.Move(finalMovement  * Time.deltaTime);
	}
}
