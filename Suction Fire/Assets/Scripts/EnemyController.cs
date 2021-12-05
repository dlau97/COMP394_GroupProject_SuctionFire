using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType {SmallEnemy, LargeEnemy}
    public EnemyType type;
    public Rigidbody2D EnemyRB;
	public float chaseSpeed = 6f;
	public float suckedSpeed = 10f;
    public float stretchDuration = 1f, unStretchDuration = 2f;
	private float startStretchTime, startUnStretchTime;
	private bool stretching;
	private bool sucking;
	private GameObject player;
	private bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        stretching = false;
		playerInRange = false;
		sucking = false;
		player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if(type == EnemyType.SmallEnemy){
			if (stretching == true) {
				StretchEnemy ();
				if (Time.time >= startStretchTime + stretchDuration) {
					stretching = false;
					startUnStretchTime = Time.time;
				}
			} else  {
				UnstretchEnemy ();
				sucking = false;
				
			}
			if(Input.GetKeyUp(KeyCode.Mouse1)){
				stretching = false;
			}
		}

    }

    	float toRadians (float degrees)
	{
		return degrees * (Mathf.PI / 180f);
	}

	float toDegrees (float radians)
	{
		return radians * (180f / Mathf.PI);
	}

	void StretchEnemy ()
	{
		Vector3 newScale = new Vector3 (1.4f, 0.2f, 1f);

		float t = (Time.time - startStretchTime) / stretchDuration;

		this.transform.localScale = Vector3.Lerp (this.transform.localScale, newScale, t);

	}
	void UnstretchEnemy ()
	{
		Vector3 newScale = new Vector3 (1f, 1f, 1f);

		float t = (Time.time - startUnStretchTime) / unStretchDuration;

		this.transform.localScale = Vector3.Lerp (this.transform.localScale, newScale, t);

	}
	void startStretching ()
	{
		stretching = true;
		startStretchTime = Time.time;
        EnemyRB.gravityScale = 0f;
	}
    void suckTowardsPlayer(){
		this.transform.position = Vector3.MoveTowards (this.transform.position, player.transform.position, suckedSpeed * Time.deltaTime);
		sucking = true;
	}
	void startUnstretching ()
	{
		stretching = false;
		startUnStretchTime = Time.time;
		sucking = false;
        EnemyRB.gravityScale = 1f;
	}

    void OnCollisionEnter2D(Collision2D other){

		if ((other.gameObject.tag == "Player") && (sucking == true)) {
			GameObject.Find ("Player").SendMessage ("AddAmmo");
			other.rigidbody.velocity = Vector3.zero;
			Destroy (this.gameObject);
		} 
		

		if (other.gameObject.tag == "NormalPlayerBullet") {

			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Enemy") {
			EnemyRB.velocity = Vector3.zero;
		}
	}
}
