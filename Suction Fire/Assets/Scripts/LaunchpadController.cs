using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchpadController : MonoBehaviour
{
	public float LaunchStrength = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Rigidbody2D objRB = other.GetComponent<Rigidbody2D> ();
            objRB.velocity = Vector3.zero;
            objRB.AddForce(new Vector2(0f, LaunchStrength), ForceMode2D.Impulse);
            this.transform.eulerAngles = Vector3.zero;
        }

	}
}
