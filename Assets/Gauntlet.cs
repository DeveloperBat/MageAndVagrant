using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauntlet : MonoBehaviour {

    DialogueSystem ds;

    void Start()
    {
        ds = GameObject.Find("Dialogue Canvas").GetComponent<DialogueSystem>();
    }

    void OnTriggerEnter2D()
    {
        DestroyObject(gameObject);
        ds.PrintDialogue("Tutorial02_02_gauntlet");
    }
}
