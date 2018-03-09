using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk : MonoBehaviour {

    DialogueSystem ds;

    void Start()
    {
        ds = GameObject.Find("Dialogue Canvas").GetComponent<DialogueSystem>();
    }

	void OnCollisionEnter2D()
    {
        DestroyObject(gameObject);
        ds.PrintDialogue("Tutorial01_03_chalk");
    }

}
