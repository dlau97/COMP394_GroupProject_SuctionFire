using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
	public float jumpHeight = 5;
	public Transform groundCheck;
	public LayerMask groundLayer;
    private bool canJump = true;
    private Rigidbody2D playerRB;
	private Transform playerT;
	private SpriteRenderer playerSR;
	private float direction = 1f; //Direction 1 = right, -1 = left;
    private int ammo = 0;
	public int maxAmmo = 5;
    public TMP_Text ammoCounter, healthCounter;
    public GameObject NormalBullet, ExplosiveBullet;
	public int playerHealth = 3;

    public float bulletSpeed = 10f;
    //private float healthpoints = 100;
    public GameObject suctionGun;

    private bool sucking = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody2D> ();
		playerT = this.gameObject.GetComponent<Transform> ();
		playerSR = this.gameObject.GetComponent<SpriteRenderer> ();
        suctionGun.SetActive (false);
		//healthpoints = 100;
		ammo = 0;
        sucking = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkDirection();
        checkMovement ();
		checkJump();
		checkRotation();
        checkMouseClicks ();
    }

    void checkMovement()
	{
		float horizontalMovement = Input.GetAxis ("Horizontal") * speed;
		Vector3 movement = new Vector3 (horizontalMovement, 0f, 0f) * Time.deltaTime;
		this.transform.Translate(movement, Space.World);
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
		if(horizontalMovement == 0f){
			playerRB.velocity = new Vector3(0f, playerRB.velocity.y, 0f);
		}
	}
	void checkDirection(){
		if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && sucking == false){
			direction  = -1f;
            playerT.localScale = new Vector3(direction, 1f,1f);
		}
		else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && sucking == false){
			direction = 1f;
            playerT.localScale = new Vector3(direction, 1f,1f);
		}
	}

	void checkRotation(){
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}

    void checkJump()
	{
		//canJump = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

		if(Input.GetKeyDown(KeyCode.Space) && canJump == true){
			playerRB.AddForce(new Vector2(0,jumpHeight), ForceMode2D.Impulse);
			canJump = false;
		}
	}

    void checkMouseClicks ()
	{

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			suctionGun.SetActive (true);
			GameObject.Find ("Cone").SendMessage ("StartGun");
            sucking = true;
            Debug.Log("sucking: " + sucking);
		} else if (Input.GetKeyUp (KeyCode.Mouse1)) {
            sucking = false;
            Debug.Log("sucking: " + sucking);
			suctionGun.SetActive (false);
		} else if (Input.GetKeyDown (KeyCode.Mouse0) && (ammo > 0)) {
			GameObject ammoClone = Instantiate (NormalBullet, this.transform.position, Quaternion.identity, this.transform);
            Rigidbody2D ammoRB = ammoClone.GetComponent<Rigidbody2D>();
         
			ammoClone.transform.localPosition = new Vector3 (1f, 0f, 0f);

			ammoClone.transform.SetParent (null);

            Vector3 dir = new Vector3 (direction, 0f, 0f);

		    ammoRB.velocity = dir * (bulletSpeed);
			ammo -= 1;
			
		}
		if(ammo == 5){
			ammoCounter.text = "Ammo: " + ammo.ToString () + " (Max)";
		}
		else{
			ammoCounter.text = "Ammo: " + ammo.ToString ();
		}
		


	}

    void AddAmmo ()
	{
		if(ammo < maxAmmo){
			ammo = ammo + 1;
		}
		else {
			ammo = maxAmmo;
		}
	}

	public void DecreaseHealth(){
		GameObject.Find ("Game Manager").SendMessage ("ShakeScreen", 0.4f);
		playerHealth--;
		healthCounter.text = "Health: "+ playerHealth.ToString();
		if(playerHealth <= 0){
			SceneManager.LoadScene("GameOver");
		}
	}

    public void EnableJump(){
		canJump = true;
	}

	public void DisableJump(){
		canJump = false;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if( other.gameObject.tag == "LargeEnemy" ){
            SceneManager.LoadScene("GameOver");
        }
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if( other.gameObject.tag == "EnemyBullet"){
			DecreaseHealth();
			Destroy(other.gameObject);
		}
	}

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "SmallEnemy" || other.gameObject.tag == "EnemyBullet"){
             playerRB.velocity = Vector3.zero;
        }


    }
}
