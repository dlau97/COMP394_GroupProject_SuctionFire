using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private int direction = 1; //Direction 1 = right, -1 = left;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody2D> ();
		playerT = this.gameObject.GetComponent<Transform> ();
		playerSR = this.gameObject.GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {
        checkDirection();
        checkMovement ();
		checkJump();
		checkRotation();
    }

    void checkMovement()
	{
		float horizontalMovement = Input.GetAxis ("Horizontal") * speed;
		Vector3 movement = new Vector3 (horizontalMovement, 0f, 0f) * Time.deltaTime;
		this.transform.Translate(movement, Space.World);
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
	}
	void checkDirection(){
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			direction  = -1;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			direction = 1;
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

    public void EnableJump(){
		canJump = true;
	}

	public void DisableJump(){
		canJump = false;
	}
}
