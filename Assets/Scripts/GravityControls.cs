using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControls : MonoBehaviour {

    public float gravityForceHorizontal;
    public float gravityForceVertical;

    // Use this for initialization
    void Start()
    {
        Physics2D.gravity = new Vector2(gravityForceHorizontal, gravityForceVertical);
    }
}
