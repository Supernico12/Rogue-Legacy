using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {

	[SerializeField]
	float lifeTime = 3;
	[SerializeField]
	float speed = 1;
	void Start()
	{
		Destroy(gameObject, lifeTime);
	}
	void Update()
	{
		transform.Translate(Vector3.up * Time.deltaTime * speed);

	}
}
