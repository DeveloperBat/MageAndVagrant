using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour {

    public float speed;
    public Vector2 direction;
    private Rigidbody2D rb2d;


    void Start () {

        rb2d = GetComponent<Rigidbody2D>();

	}
	
    void FixedUpdate()
    {
        rb2d.velocity = direction * speed;
    }

    public void Direction(Vector3 startingDirection)
    {

        this.direction = startingDirection;

      
    }

    void OnBecameInvisible() // Används temporärt. Använd hellre destroy med tid ist.
    {
        Destroy(gameObject);
    }
}
