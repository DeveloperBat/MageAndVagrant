using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Lever : MonoBehaviour {

    DialogueSystem ds;
    bool dialogue = false;

    public delegate void ToggleKey();
    public event ToggleKey OnKeyToggled;

    private Interactable _interactable;

    private void Start()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.OnInteract += ToggleLever;

        ds = GameObject.Find("Dialogue Canvas").GetComponent<DialogueSystem>();
    }

    private void ToggleLever()
    {
        OnKeyToggled();
        if (dialogue == false)
        {
            dialogue = true;
            ds.PrintDialogue("Tutorial01_04_open");
        }
    }
}
