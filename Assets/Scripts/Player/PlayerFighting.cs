using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighting : MonoBehaviour {


	[SerializeField]
	GameObject arrow;
	[SerializeField]
	Transform arrowSpawnPosition;
	[SerializeField]
	float ArrowForce;

	CharacterStats stats;
	CapsuleCollider2D colliderplayer;
	PlayerMotor playerMotor;
	float lastAttack;
	Animator animator;

	[Header("Animations")]
	[SerializeField] AnimationClip replazableAttack;
	[SerializeField] AnimationClip replazableMove;
	[SerializeField] AnimationClip[] playerAnimationsAttack;
	[SerializeField] AnimationClip[] playerAnimationsMove;
	AnimatorOverrideController overrideAnimator;


	

	void Start()
	{

		animator = GetComponentInChildren<Animator>();
		overrideAnimator = new AnimatorOverrideController(animator.runtimeAnimatorController);
		animator.runtimeAnimatorController = overrideAnimator;
		stats = GetComponent<CharacterStats>();
		playerMotor = GetComponent<PlayerMotor>();
		colliderplayer = GetComponent<CapsuleCollider2D>();
	}
	void SwordAttack()
	{
		if (Input.GetButton("Attack"))
		{
			if (lastAttack <= 0)
			{
				overrideAnimator[replazableAttack.name] = playerAnimationsAttack[0];
				animator.SetTrigger("attack");
				lastAttack = stats.GetAttackSpeed;
				playerMotor.SetCanMove(false);
			}

		}

	}

	public void FinishedAttacking()
	{
		playerMotor.SetCanMove(true);
		/*
		Collider2D[] inRange;
		ContactFilter2D contactFilter = new ContactFilter2D();
		attackArea.OverlapCollider(contactFilter, inRange);
		*/
		float direction = transform.localScale.x;
		Collider2D[] results = new Collider2D[10];
		Physics2D.OverlapBoxNonAlloc(stats.offset * direction + transform.position, stats.radious, 0, results);
		foreach (Collider2D result in results)
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

	void BowAttack()
	{

		if (Input.GetButton("BowAttack"))
		{
			if (lastAttack <= 0)
			{
				// ChangeAttack Animation
				overrideAnimator[replazableAttack.name] = playerAnimationsAttack[1];
				animator.SetTrigger("attack");
				lastAttack = stats.GetAttackSpeed;
				playerMotor.SetCanMove(false);
			}
		}
	}


	public void FinishAttackingBow()
	{
		playerMotor.SetCanMove(true);
		float direction = transform.localScale.x;
		Rigidbody2D  rb =Instantiate(arrow, arrowSpawnPosition.position, Quaternion.identity).GetComponent<Rigidbody2D>();
		rb.AddForce(new Vector2(ArrowForce * direction, 0 ),ForceMode2D.Impulse);
		rb.GetComponent<ArrowScript>().SetDamage(stats.GetDamage);
		rb.transform.localScale *= transform.localScale.x;
		// Bow Attack 
	}

	void Update()
	{
		SwordAttack();
		BowAttack();
		lastAttack -= Time.deltaTime;
	}
}
