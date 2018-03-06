using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour {

    public bool isLit;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isLit)
        {
            animator.SetBool("isLit", true);
        }
        else
        {
            animator.SetBool("isLit", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Fire Projectile")
        {
            isLit = true;
        }
        else if (collider.gameObject.tag == "Wind Projectile")
        {
            isLit = false;
        }
    }
}
