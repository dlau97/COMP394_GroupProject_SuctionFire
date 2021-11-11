using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionController : MonoBehaviour {

	private float startScaleTime;
	public float scaleDuration = 1f;
	private bool scaling = false;
	private GameObject suctionGun;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (scaling == true) {
			scaleSuctionCone ();
			if (Time.time >= startScaleTime + scaleDuration) {
				scaling = false;
			}
		}
	}

	void StartGun(){
		suctionGun = GameObject.Find ("SuctionGun");
		suctionGun.transform.localScale = new Vector3 (0.1f, 0.1f, 1f);
		startScaleTime = Time.time;
		scaling = true;
	}

	void scaleSuctionCone(){
		Vector3 newScale = new Vector3 (1, 1f, 1f);


		float t = (Time.time - startScaleTime) / scaleDuration;

		suctionGun.transform.localScale = Vector3.Lerp (this.transform.localScale, newScale, t);
	}


	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("startStretching");
		}

	}



	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("SuckTowardsPlayer");
		}
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("startUnstretching");
			Debug.Log ("UnStretch");
		}

	}


}

//Final Cone X Dimension: 0.0305
//Final Cone Y Dimension: 0.055

//Initial Cone X Dimension: 0.003607184f
//Initial Cone Y Dimension:  0.007167332f


