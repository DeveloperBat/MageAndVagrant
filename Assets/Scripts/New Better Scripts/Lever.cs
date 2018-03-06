using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Lever : MonoBehaviour {

    public delegate void ToggleKey();
    public event ToggleKey OnKeyToggled;

    private Interactable _interactable;

    private void Start()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.OnInteract += ToggleLever;
    }

    private void ToggleLever()
    {
        OnKeyToggled();
    }
}
