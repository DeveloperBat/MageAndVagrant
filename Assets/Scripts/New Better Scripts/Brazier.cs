using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : MonoBehaviour {

    public bool isBurning;
    public List<string> igniters;
    public List<string> snuffers;

    public delegate void ToggleKey();
    public event ToggleKey OnKeyToggled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (igniters.Contains(collision.gameObject.tag) && !isBurning)
        {
            Toggle();
        }
        else if (snuffers.Contains(collision.gameObject.tag) && isBurning)
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        OnKeyToggled();
        isBurning = !isBurning;
    }

}
