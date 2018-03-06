using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(RectTransform))]
public class BreakablePlatform : MonoBehaviour {

    public float fallGravity;
    public float fallLength;
    public float fallDelay;
    public bool falling;
    public List<string> interactables;

    private Rigidbody2D _rigidBody;
    private RectTransform _rectTransform;
    private float _timeLeft;
    private float _startYPos;

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _startYPos = _rectTransform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(interactables.Contains(collision.gameObject.tag) && collision.relativeVelocity.y < 0)
        {
            _timeLeft = fallDelay;
            falling = true; 
        }
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        Fall();
        Break();
    }

    private void Fall()
    {
        if(_timeLeft < 0 && falling)
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody.gravityScale = fallGravity;
        }
    }

    private void Break()
    {
        if(_rectTransform.position.y < _startYPos - fallLength)
        {
            Destroy(gameObject);
        }
    }

}
