using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{


    private Vector2 projDir;

    public Vector3 reflectedVector;

    public Transform originalObject;
    public Transform reflectedObject;


    void Start()
    {
        projDir = gameObject.GetComponent<Travel>().direction;
    }

    void Update()
    {
        projDir = gameObject.GetComponent<Travel>().direction;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            reflectedVector = Vector3.Reflect(projDir, coll.contacts[0].normal);
            gameObject.GetComponent<Travel>().Direction(reflectedVector);
        }
    }
}
