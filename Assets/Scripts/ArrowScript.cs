using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	float damage;
	// 10 = Enemy 8 = Ground

	public void SetDamage(float amount)
	{
		damage = amount;
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
