using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windspell : MonoBehaviour {

    public float boostForce;

    private GameObject vagrantObj;
    private Rigidbody2D vagrantVel;
    private Vector2 contactVector2;
    private float xDir;
    private float yDir;

    void Start ()
    {
        vagrantObj = GameObject.Find("Vagrant");
        vagrantVel = vagrantObj.GetComponent<Rigidbody2D>();
	}
	

     void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Vagrant")
        {
          
            Vector3 contactVector2 = collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y);

            contactVector2 = contactVector2.normalized;

            vagrantVel.velocity = contactVector2 * boostForce;

            Destroy(gameObject);


            /*
            contactVector2 = collision.contacts[0].point;

            if (contactVector2.x > transform.position.x)
            {
                xDir = 1;
            }
            else
            {
                xDir = -1;
            }

            if(contactVector2.y > transform.position.y)
            {
                yDir = 1;
            }
            else
            {
                yDir = -1;
            }

            
            Destroy(gameObject);
            */
        }

    }
}
