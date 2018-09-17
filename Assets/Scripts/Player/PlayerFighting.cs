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
	[SerializeField]
	float combatTime = 5;
    [SerializeField]
    Weapon weapon1;
	[SerializeField]
	float combatCycleReset; 



	
	int currentAttackCycle = 3;
    int cycleLenght = 3;
	CharacterStats stats;
	CapsuleCollider2D colliderplayer;
	PlayerMotor playerMotor;
	float combatTimer;
	float lastAttack = -10;
	float lastCombatCycle;
    
	public bool isOnCombat { get; private set; }
	Animator animator;

	[Header("Animations")]
	[SerializeField] AnimationClip replaceableAttack;
	[SerializeField] AnimationClip replaceableMove;
	[SerializeField] AnimationClip replasceableIdle;
	[SerializeField] AnimationClip[] playerAnimationsAttack;
	[SerializeField] AnimationClip[] playerAnimationsMove;
	[SerializeField] AnimationClip[] playerAnimationsIdle;
	AnimatorOverrideController overrideAnimator;

	

	

	void Start()
	{

		animator = GetComponentInChildren<Animator>();
		overrideAnimator = new AnimatorOverrideController(animator.runtimeAnimatorController);
		animator.runtimeAnimatorController = overrideAnimator;
		stats = GetComponent<CharacterStats>();
		playerMotor = GetComponent<PlayerMotor>();
		colliderplayer = GetComponent<CapsuleCollider2D>();
		currentAttackCycle = weapon1.combatPattern.Length ;
		cycleLenght = currentAttackCycle;
	}
	void SwordAttack()
	{
		if (Input.GetButton("Attack"))
		{
			if (combatTimer <= 0)
			{
				lastAttack = Time.time;
				overrideAnimator[replaceableAttack.name] = weapon1.animations[(currentAttackCycle  )% cycleLenght];
				animator.SetFloat("attackSpeed", 1 / weapon1.combatPattern[currentAttackCycle % cycleLenght]);
				animator.SetTrigger("attack");
				combatTimer = weapon1.combatPattern[(currentAttackCycle ) % cycleLenght  ];
				Debug.Log(combatTimer.ToString());
				playerMotor.SetCanMove(false);
				lastCombatCycle = combatCycleReset;
                
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
					targetstats.TakeDamage(weapon1.baseDamage * weapon1.combatPattern[(currentAttackCycle ) % cycleLenght]);
				//Rigidbody2D rb = result.GetComponent<Rigidbody2D>();
				 //if (rb != null)
					//rb.AddForce(new Vector2(stats.GetKnockBack.x * direction, stats.GetKnockBack.y), ForceMode2D.Impulse);

			}

		}
	
        currentAttackCycle++;
		
	}

	void BowAttack()
	{

		if (Input.GetButton("BowAttack"))
		{
			if (combatTimer<= 0)
			{
				// ChangeAttack Animation
				lastAttack = Time.time;
				overrideAnimator[replaceableAttack.name] = playerAnimationsAttack[1];
				animator.SetTrigger("attack");
				combatTimer= stats.GetAttackSpeed;
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
		combatTimer -= Time.deltaTime;
		lastCombatCycle -= Time.deltaTime;

		if (Time.time - lastAttack < combatTime)
		{
			isOnCombat = true;
		}else
		{
			isOnCombat = false;
		}
		if(lastCombatCycle < 0)
		{
			currentAttackCycle = cycleLenght;
		}

		if (isOnCombat)
		{
			overrideAnimator[replaceableMove.name] = playerAnimationsMove[1];
			overrideAnimator[replasceableIdle.name] = playerAnimationsIdle[1];
		}else {

			overrideAnimator[replaceableMove.name] = playerAnimationsMove[0];
			overrideAnimator[replasceableIdle.name] = playerAnimationsIdle[0];
		}
		
	}
}
