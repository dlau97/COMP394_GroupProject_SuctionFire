using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform playerT;
	public float followTime = 0.15f;
	public float FollowSpeed = 2f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame


	void Update()
	{
		Vector3 newPosition = playerT.position;
		newPosition.z = -10;
		newPosition.y = 2f;
		newPosition.x = newPosition.x + 2f;
		this.transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
	}
}
