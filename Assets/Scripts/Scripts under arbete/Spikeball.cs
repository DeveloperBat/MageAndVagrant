using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeball : MonoBehaviour {




	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {

            Destroy(collision.gameObject);

        }

    }
}
