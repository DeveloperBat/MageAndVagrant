using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator animator;

    public float speed;
    public float jumpHeight;
    public string player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(player + "Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.position += speed * movement;

        if (Input.GetButtonDown(player + "Jump"))
        {
            rb.velocity = new Vector2(0, jumpHeight);
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis(player + "Horizontal");

        if (moveHorizontal > 0)
        {
            animator.SetBool("facingRight", true);
            animator.SetBool("isMoving", true);
        }
        else if (moveHorizontal < 0)
        {
            animator.SetBool("facingRight", false);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}