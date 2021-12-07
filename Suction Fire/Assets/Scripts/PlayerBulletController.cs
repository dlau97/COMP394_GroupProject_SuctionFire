using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public enum BulletType {Normal, Explosive}
    public BulletType type = BulletType.Normal;
	private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D> ();

        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate (new Vector3 (0, 0, 1000f) * Time.deltaTime); //Give bullet rotation
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall"){
            Destroy(this.gameObject);
        }

	}
}
