using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PressurePlate : MonoBehaviour {

    public delegate void ToggleKey();
    public event ToggleKey OnKeyToggled;

    public List<string> triggers;
    public float sinkLength = 0.5f;

    private GameObject _pressureSource;
    private RectTransform _rt;
    private Vector3 _startPos;
    private Vector3 _sinkPos;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        _startPos = _rt.position;
        _sinkPos = new Vector3(_startPos.x, _startPos.y - sinkLength, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(triggers.Contains(collision.gameObject.tag) && _pressureSource == null)
        {
            _pressureSource = collision.gameObject;
            _rt.position = _sinkPos;
            OnKeyToggled();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == _pressureSource)
        {
            _pressureSource = null;
            _rt.position = _startPos;
            OnKeyToggled();
        }
    }


}
