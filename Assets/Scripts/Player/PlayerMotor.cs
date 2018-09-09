using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMotor : MonoBehaviour {


	CapsuleCollider2D colliderplayer;
	CharacterStats stats;
	[SerializeField]
	float movementSpeed = 1;
	[SerializeField]
	float gravity = 10;
	[SerializeField]
	float jumpStrenght = 8;

	int layerMask;
	ContactFilter2D filter;

	[SerializeField]
	Collider2D attackArea;

	float lastAttack;
	bool canMove = true;
	bool isGrounded;
	Animator animator;
	float attackSpeed;
	Vector2 movement = Vector2.zero;

	// Use this for initialization
	void Start() {
		colliderplayer = GetComponent<CapsuleCollider2D>();
		animator = GetComponentInChildren<Animator>();
		stats = GetComponent<CharacterStats>();
		attackSpeed = stats.GetAtqueSpeed;

		
		filter.useTriggers = false;
		filter.SetLayerMask ( Physics2D.GetLayerCollisionMask(gameObject.layer));
		filter.useLayerMask = true;

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
			if (result != null && result != colliderplayer)
			{
				
					CharacterStats targetstats = result.GetComponent<CharacterStats>();
					if (targetstats != null)
						targetstats.TakeDamage(stats.GetDamage);
					Rigidbody2D rb = result.GetComponent<Rigidbody2D>();
					if (rb != null)
						rb.AddForce(new Vector2(stats.GetKnockBack.x * direction, stats.GetKnockBack.y), ForceMode2D.Impulse);
				
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


	void IsGrounded()
	{
		// isGrounded =Physics.CheckCapsule(colliderplayer.bounds.center, new Vector3(colliderplayer.bounds.center.x, colliderplayer.bounds.min.y - 0.1f, colliderplayer.bounds.center.z), 0.9f);

		//get the radius of the players capsule collider, and make it a tiny bit smaller than that
		float radius = colliderplayer.size.x * 0.9f;
		//get the position (assuming its right at the bottom) and move it up by almost the whole radius
		Vector3 pos = transform.position - Vector3.up * (radius * 0.9f);
		
		//returns true if the sphere touches something on that layer
		Collider2D[] results = new Collider2D[16];
		//int cantOBJ = Physics2D.OverlapCircleNonAlloc(pos, radius, results,layerMask);
		int cantOBJ = Physics2D.OverlapCircle(pos, radius, filter, results);
		if (cantOBJ > 0)
		{
			isGrounded = true;

		} else
		{
			isGrounded = false;

		}
		foreach (Collider2D result in results)
		{
			if (result != null)
			{
				Debug.Log(result.name);
			}
		}
	}
		// Update is called once per frame
		void FixedUpdate() {

			Move();
			Attack();
			IsGrounded();

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
			//controller.Move(finalMovement  * Time.deltaTime);
		}
	
}
