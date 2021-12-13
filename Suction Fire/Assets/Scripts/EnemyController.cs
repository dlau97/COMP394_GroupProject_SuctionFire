using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { SmallEnemy, LargeEnemy }
    public EnemyType type;
    public Rigidbody2D EnemyRB;
    public float chaseSpeed = 6f;
    public float suckedSpeed = 10f;
    public float stretchDuration = 1f, unStretchDuration = 2f;
    private float startStretchTime, startUnStretchTime, startHitTime;
    private bool stretching;
    private bool sucking;
    private GameObject player;
    public float smallMoveSpeed = 5f;
    public float largeMoveSpeed = 3f;
    private int direction = 0;
    //Directions 1 = Right ; Left = 2
    private bool initialPlatform = false;
    private int hitpoints = 2;

    public GameObject hitSprite, smallDeathFX, largeDeathFX;


    // Start is called before the first frame update
    void Start()
    {
        stretching = false;
        sucking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyRB = this.gameObject.GetComponent<Rigidbody2D>();
        startHitTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (type == EnemyType.SmallEnemy)
        {
            if (stretching == true)
            {
				
                StretchEnemy();
                if (Time.time >= startStretchTime + stretchDuration)
                {
                    stretching = false;
                    startUnStretchTime = Time.time;
                }
            }
            else
            {
                UnstretchEnemy();
                sucking = false;
                if (initialPlatform == true)
                {
                    if (direction == 1)
                    { //direction right

                        EnemyRB.velocity = new Vector3(smallMoveSpeed, EnemyRB.velocity.y, 0f);


                    }
                    else if (direction == 2)
                    { //direction left

                        EnemyRB.velocity = new Vector3((-1f) * smallMoveSpeed, EnemyRB.velocity.y, 0f);

                    }
                }

            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                stretching = false;
            }
        }
        else
        {
            if (initialPlatform == true){
                if (direction == 1) { //direction right
                    EnemyRB.velocity = new Vector3(largeMoveSpeed, EnemyRB.velocity.y, 0f);
                }
                else if (direction == 2) { //direction left
                    EnemyRB.velocity = new Vector3((-1f) * largeMoveSpeed, EnemyRB.velocity.y, 0f);
                }
            }
            if(Time.time >= startHitTime + 0.1f){
                hitSprite.SetActive(false);
            }

        }

    }

    void GroundMovement()
    {

    }

    float toRadians(float degrees)
    {
        return degrees * (Mathf.PI / 180f);
    }

    float toDegrees(float radians)
    {
        return radians * (180f / Mathf.PI);
    }

    void StretchEnemy()
    {
        Vector3 newScale = new Vector3(1.4f, 0.2f, 1f);

        float t = (Time.time - startStretchTime) / stretchDuration;

        this.transform.localScale = Vector3.Lerp(this.transform.localScale, newScale, t);

    }
    void UnstretchEnemy()
    {
        Vector3 newScale = new Vector3(1f, 1f, 1f);

        float t = (Time.time - startUnStretchTime) / unStretchDuration;

        this.transform.localScale = Vector3.Lerp(this.transform.localScale, newScale, t);

    }
    void startStretching()
    {
        stretching = true;
        startStretchTime = Time.time;
        EnemyRB.gravityScale = 0f;
		EnemyRB.velocity = Vector3.zero;
    }
    void suckTowardsPlayer()
    {
		
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, suckedSpeed * Time.deltaTime);
        sucking = true;
    }
    void startUnstretching()
    {
        stretching = false;
        startUnStretchTime = Time.time;
        sucking = false;
        EnemyRB.gravityScale = 1f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (sucking)
            {
                GameObject.Find("Player").SendMessage("AddAmmo");
                other.rigidbody.velocity = Vector3.zero;
                Destroy(this.gameObject);
            }
            else
            {
                GameObject.Find("Player").SendMessage("DecreaseHealth");
            }

        }

        if (other.gameObject.tag == "NormalPlayerBullet")
        {
            if (this.gameObject.tag == "LargeEnemy")
            {
                hitpoints--;
                hitSprite.SetActive(true);
                startHitTime = Time.time;
                if (hitpoints <= 0)
                {
                    Destroy(Instantiate(largeDeathFX, this.transform.position, Quaternion.identity), 5f);
                    Destroy(this.gameObject);
                    
                }
            }
            else
            {
                Destroy(Instantiate(smallDeathFX, this.transform.position, Quaternion.identity), 5f);
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Ground")
        {
            if (initialPlatform == false)
            {
                direction = Random.Range(1, 3); //Randomise a direction (1 or 2)
                initialPlatform = true;
            }
            transform.rotation = Quaternion.identity;
        }
        else if (other.gameObject.tag == "Wall")
        {
            if (direction == 1)
            {
                direction = 2;
            }
            else if (direction == 2)
            {
                direction = 1;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyRB.velocity = Vector3.zero;
        }
    }
}
