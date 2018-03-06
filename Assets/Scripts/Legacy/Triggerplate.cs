using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerplate : MonoBehaviour {

    SpriteRenderer spriteren;
    public bool doorUnlocked;

    void Start()
    {
        spriteren = GetComponent<SpriteRenderer>();
        doorUnlocked = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Mage")
        {
            spriteren.color = new Color(0f, 1f, 0f);
            doorUnlocked = true;
        }
    }

}
