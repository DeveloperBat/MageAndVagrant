using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {

    public List<string> triggers;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(triggers.Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    // Add an event that changes a variable in the game script
}
