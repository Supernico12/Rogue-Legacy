using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundScrolling : MonoBehaviour {
	[SerializeField]
	GameObject parent;
	
	[SerializeField] float[] scrollSpeed;
	[SerializeField] float tileSize;
	[SerializeField] Transform[] layers = new Transform[10];
	Vector3[] startingPosition = new Vector3 [10];
	float direction = 0;
	void Start()
	{
		for (int i = 0; i < startingPosition.Length; i++)
		{
			startingPosition[i] = layers[i].position;
		}
			//layers = GetComponentsInChildren<Transform>();
		}


	public void SetDirection (float newdirection)
	{
		direction = newdirection;
	}
	void Update()
	{
		//Debug.Log(layers[6].position.x);
		for (int i = 0; i < scrollSpeed.Length; i++) { 

			
		//float newPosition = Mathf.Repeat(direction * scrollSpeed[i], tileSize);
		if ( -layers[i].position.x > tileSize)
			{
				layers[i].position = startingPosition[i];
				
			}
			layers[i].Translate(  Vector3.left * direction* scrollSpeed[i] * Time.deltaTime);
		}
	}
}
