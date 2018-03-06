using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMovement : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;
    private Vector3 direction;
    private Collider2D projectileCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * direction;
        projectileCollider = GetComponent<Collider2D>();
    }

    public void Direction(Vector3 startingDirection)
    {
        this.direction = startingDirection;
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Shield")
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (collision.collider.tag == "Wall" ||
                 collision.collider.tag == "Ground" ||
                 collision.collider.tag == "Platform")
        {
            Destroy(gameObject);
        }
        
        else if (collision.collider.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, projectileCollider);
        } 
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
