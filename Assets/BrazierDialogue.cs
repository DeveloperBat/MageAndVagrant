using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazierDialogue : MonoBehaviour {

    Brazier brazier;
    DialogueSystem ds;

    public string dialogue;
    bool activated = false;

    void Start()
    {
        brazier = GetComponent<Brazier>();
        ds = GameObject.Find("Dialogue Canvas").GetComponent<DialogueSystem>();
    }

    void Update()
    {
        if (brazier.isBurning && activated == false)
        {
            activated = true;
            ds.PrintDialogue(dialogue);
        }
    }

}
