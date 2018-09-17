using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	[SerializeField] float lifeTime;
	float damage;
	Rigidbody2D rb;
	// 10 = Enemy 8 = Ground


	void Start(){
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(GravityStart());
		
	}
	public void SetDamage(float amount)
	{
		damage = amount;
	}
	IEnumerator GravityStart(){

		rb.gravityScale = 0;
		yield return new WaitForSeconds(lifeTime);
		rb.gravityScale = 5;
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if ( col.tag != null)
		{
			Debug.Log(col.name);

			if ( col.gameObject.layer == 10)
			{
				col.GetComponent<CharacterStats>().TakeDamage(damage);
				col.GetComponent<EnemyController>().SetAlerted(true);
				Destroy(gameObject);
			}
			if ( col.gameObject.layer == 8)
			{
				Destroy(gameObject);
			}
		}
	}
}
