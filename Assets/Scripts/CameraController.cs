using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Transform target;
	Camera cam;
	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;
		cam = GetComponent<Camera>();
	}

	void Update()
	{
		Vector3 cameraPosition = new Vector3(target.transform.position.x, 0, -10);
		cam.transform.position = cameraPosition;
	}
	
	
}
