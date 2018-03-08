using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    DialogueSystem ds;
    public string dialogue;
    
    void Start()
    {
        ds = GameObject.Find("").GetComponent<DialogueSystem>();
    }

    void OnTriggerEnter2D()
    {
        ds.PrintDialogue(dialogue);
        DestroyObject(gameObject);
    }

}
