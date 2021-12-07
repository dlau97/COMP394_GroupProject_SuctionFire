using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	private Transform turretT;
	private Transform playerT;
	public float rotationSpeed = 0.5f;
	public GameObject turretProjectile;
    public float shootTimeDelay = 0.2f;
    private float currTime = 0f;
    private float startTime;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        health = 100;
        turretT = gameObject.transform;
        playerT = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        if (Time.time > currTime + shootTimeDelay) {

            ShootBullet ();

        }
        RotateToPlayer ();
    }

    private void CheckHealth(){
        if (health <= 0) {
			Destroy (this.gameObject); //destroy script so the turret cant shoot or move anymore.
		}
    }

    private void CheckShooting(){
        if (Time.time > currTime + shootTimeDelay) {

            ShootBullet ();

        }
    }

    private void ShootBullet(){
		Quaternion turretAngle = Quaternion.Euler (turretT.eulerAngles);
		GameObject FrontBullet = Instantiate (turretProjectile, new Vector3 (0f, 0f, 0f), turretAngle, turretT);
		FrontBullet.transform.localPosition = new Vector3 (0.75f, 0f, 0f);
		FrontBullet.gameObject.transform.SetParent (null);
		currTime = Time.time;
    }

    private void RotateToPlayer(){
        Vector3 dir = playerT.position - turretT.position;

		//Calculate the angle required for the turret to look at the player.
		float playerAngle = toDegrees (Mathf.Atan2 (dir.y, dir.x));

		Quaternion rot = Quaternion.AngleAxis (playerAngle, Vector3.forward);

		turretT.rotation = Quaternion.Slerp (turretT.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    	public float toDegrees (float radians)
	{
		return radians * (180 / Mathf.PI);
	}
}
