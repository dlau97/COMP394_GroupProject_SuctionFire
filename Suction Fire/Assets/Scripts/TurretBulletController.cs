using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletController : MonoBehaviour
{
	public float speed = 5;
	private Rigidbody2D projectileRB;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        projectileRB = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement ();
			if (Time.time > startTime + 5f) {
				Destroy (this.gameObject);
			}
    }

    void BulletMovement ()
	{
		float angle = this.transform.eulerAngles.z;
		float xDir = Mathf.Cos (toRadians (angle));
		float yDir = Mathf.Sin (toRadians (angle));
		Vector3 dir = new Vector3 (xDir, yDir, 0f);

		projectileRB.velocity = dir * (speed);
	}

    float toRadians (float degrees)
	{
		return degrees * (Mathf.PI / 180f);
	}

	float toDegrees (float radians)
	{
		return radians * (180f / Mathf.PI);
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall"){
            Destroy(this.gameObject);
        }
        
    }
}
