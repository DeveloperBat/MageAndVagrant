using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WallPlatforms : MonoBehaviour {

    public bool isActive;
    public float closedZPosition;
    public float openedZPosition;
    public float moveSmoothness;
    public List<GameObject> triggers;

    private BoxCollider2D _bc2d;
    private Brazier _brazier;
    private PressurePlate _pressurePlate;
    private Lever _lever;
    private Vector3 _vel = Vector3.zero;
    private Vector3 _closedPos;
    private Vector3 _openedPos;

    private void Start()
    {
        _bc2d = GetComponent<BoxCollider2D>();
        SubscribeEvent();
        _closedPos = new Vector3(transform.position.x, transform.position.y, closedZPosition);
        _openedPos = new Vector3(transform.position.x, transform.position.y, openedZPosition);
    }

    private void Update()
    {
        _bc2d.enabled = isActive;
        MovePlatform();
    }

    private void SubscribeEvent()
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            switch (triggers[i].gameObject.tag)
            {
                case "Brazier":
                    _brazier = triggers[i].GetComponent<Brazier>();
                    _brazier.OnKeyToggled += TogglePlatform;
                    break;
                case "Lever":
                    _lever = triggers[i].GetComponent<Lever>();
                    _lever.OnKeyToggled += TogglePlatform;
                    break;
                case "PressurePlate":
                    _pressurePlate = triggers[i].GetComponent<PressurePlate>();
                    _pressurePlate.OnKeyToggled += TogglePlatform;
                    break;
            }
        }
    }

    private void TogglePlatform()
    {
        isActive = !isActive;
    }

    private void MovePlatform()
    {
        if(isActive)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _openedPos, ref _vel, moveSmoothness);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _closedPos, ref _vel, moveSmoothness);
        }
    }



}
