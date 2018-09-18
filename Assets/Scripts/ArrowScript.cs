using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	[SerializeField] float lifeTime;
	float damage;
	Rigidbody2D rb;
	[SerializeField]
	float speed;
	bool isFalling;
	[SerializeField]
	float roateSpeed;
	[SerializeField]
	float maxRotation;
	float currentRot;
	// 10 = Enemy 8 = Ground


	void Start(){
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(GravityStart());
		
	}
	public void SetDamage(float amount)
	{
		damage = amount;
	}
	public void SetSpeed(float amount){
		speed = amount;
	}
	IEnumerator GravityStart(){

	
		yield return new WaitForSeconds(lifeTime);
		
		isFalling = true;
		transform.Rotate(0,0,-.5f);
	}
	void Update(){
		
		if(isFalling){
			if(transform.eulerAngles.z > maxRotation){

			currentRot -= Time.deltaTime * roateSpeed;
			transform.Rotate(0,0, currentRot  * (speed < 0?  -speed: speed ) / speed );
			//Debug.Log(transform.eulerAngles.z);

			
			}
		}
			transform.Translate((transform.right )* speed * Time.deltaTime ,Space.World);
			//Debug.Log( (transform.right + Vector3.right ).x /2 * Time.deltaTime * speed );

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
