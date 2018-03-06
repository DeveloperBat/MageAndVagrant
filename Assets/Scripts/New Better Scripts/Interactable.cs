using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public delegate void ActivateInteractable();
    public event ActivateInteractable OnInteract;

    public void ToggleInteractive()
    {
        OnInteract();
    }

}
