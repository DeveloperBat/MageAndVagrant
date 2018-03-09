using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public bool isOpen;
    public bool liftOnOpen;
    public float liftHeight;
    public float liftSmooth;
    public List<GameObject> triggers;

    private Collider2D _collider;
    private Brazier _brazier;
    private PressurePlate _pressurePlate;
    private Lever _lever;
    private Vector3 _liftPos;
    private Vector3 _closePos;
    private Vector3 _openVel = Vector3.zero;

    private void Start()
    {
        SubscribeEvent();
        _collider = GetComponent<Collider2D>();
        _liftPos = new Vector3(transform.position.x, transform.position.y + liftHeight, transform.position.z);
        _closePos = transform.position;
    }

    private void OnDisable()
    {
        //UnsubscribeEvent();
    }

    private void SubscribeEvent()
    {
        for(int i = 0; i < triggers.Count; i++)
        {
            switch (triggers[i].gameObject.tag)
            {
                case "Brazier":
                    _brazier = triggers[i].GetComponent<Brazier>();
                    _brazier.OnKeyToggled += ToggleGate;
                    break;
                case "Lever":
                    _lever = triggers[i].GetComponent<Lever>();
                    _lever.OnKeyToggled += ToggleGate;
                    break;
                case "PressurePlate":
                    _pressurePlate = triggers[i].GetComponent<PressurePlate>();
                    _pressurePlate.OnKeyToggled += ToggleGate;
                    break;
            }
        }
    }

    /*private void UnsubscribeEvent()
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            switch (triggers[i].gameObject.tag)
            {
                case "Brazier":
                    _brazier.OnKeyToggled += ToggleGate;
                    break;
                case "Lever":
                    _lever.OnKeyToggled += ToggleGate;
                    break;
                case "PressurePlate":
                    _pressurePlate.OnKeyToggled += ToggleGate;
                    break;
            }
        }
    }*/

    private void Update()
    {
        if(liftOnOpen)
        {
            MoveGate();
        }
    }

    private void ToggleGate()
    {
        isOpen = !isOpen;
        //_collider.enabled = !isOpen;
    }

    private void MoveGate()
    {

        if(isOpen)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _liftPos, ref _openVel, liftSmooth);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _closePos, ref _openVel, liftSmooth);
        }
    }

    // Add Delays

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 toLine = new Vector3(transform.position.x, transform.position.y + liftHeight, transform.position.z);

        Gizmos.DrawLine(transform.position, toLine);
    }
}
