using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeController : MonoBehaviour
{
	private bool shaking = false;
	private float shakeDuration = 1f;
	public float shakeIntensity = 0.5f;
	public AnimationCurve decreaseFactor;
	private float startTime;
	public Transform cameraTransform;
	private Vector3 original_pos;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		original_pos = cameraTransform.position;
		if (shaking == true) {

			float t = (Time.time - startTime) / shakeDuration; 
			float amount = shakeIntensity * decreaseFactor.Evaluate (t);
			Vector3 randomVector = Random.insideUnitCircle;

			cameraTransform.localPosition = original_pos + randomVector * amount;

			if(Time.time >= startTime + shakeDuration){
				cameraTransform.position = original_pos;
				shaking = false;
			}

		}

	}

	void ShakeScreen(float time){
		shakeDuration = time;
		startTime = Time.time;
		shaking = true;
	}
}
