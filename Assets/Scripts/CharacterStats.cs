using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CharacterStats: MonoBehaviour {


	[SerializeField] float health = 100;
	[SerializeField] float Damage = 10;
	[SerializeField] float attackSpeed = 1;
	[SerializeField] float armor = 0;
	[SerializeField] TextMeshPro textmesh;
	[SerializeField] Vector2 knockBack;


	float currentHealth;
	
	[Header("AttackArea")]
	public Vector3 offset;
	public Vector2 radious;
	

	void Start()
	{
		currentHealth = health;
	}

	public Vector2 GetKnockBack
	{
		get { return knockBack; }
	}
	public float GetAtqueSpeed
	{
		get { return attackSpeed; }
	}

	public float GetDamage
	{
		get { return Damage; }
	}

	public float GetCurrentHealth
	{
		get { return currentHealth; }
	}

	public void TakeDamage(float damage)
	{
		damage -= armor;
		damage = Mathf.Clamp(damage, 1, int.MaxValue);
		currentHealth -= damage;

		if (currentHealth <= 0)
			Die();

		  TextMeshPro damagetext =Instantiate(textmesh, transform.position, Quaternion.identity);
		damagetext.text = "-" + damage.ToString();
		StartCoroutine(DamageAnimation());
		
	}


	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position + offset, radious);
		

	}

	void Die()
	{
		Destroy(gameObject);
	}


	IEnumerator DamageAnimation()
	{
		SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
		Color32 lastcolor = renderer.color;
		renderer.color = Color.red;
		
		yield return new WaitForSeconds(1);
		renderer.color = lastcolor;
	}
}
