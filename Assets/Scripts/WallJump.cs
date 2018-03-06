using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManualMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class WallJump : MonoBehaviour {

    public float slideSpeed;
    public float slideTime;

    private Rigidbody2D _rigidBody;
    private ManualMovement _playerMovement;
    private float _timer;
    private GameObject _prevWall;
    private bool _canSlide;

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _playerMovement = gameObject.GetComponent<ManualMovement>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if(_playerMovement.isGrounded)
        {
            _canSlide = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && collision.gameObject != _prevWall)
        {
            _timer = slideTime;
            _prevWall = collision.gameObject;
            _canSlide = true;
            _playerMovement.jumped = false;
            //_playerMovement.canJump = true;
        }
        else
        {
            _canSlide = false;
            _prevWall = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == _prevWall && _playerMovement.canJump && _rigidBody.velocity.y == 0 && _playerMovement.isGrounded == true)
        {
            _prevWall = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" && _timer > 0 && _canSlide && collision.gameObject == _prevWall && _playerMovement.jumped == false)
        {
            Slide();
            _playerMovement.canJump = true;
        }
    }

    private void Slide()
    {
        Vector2 playerVelocity = _rigidBody.velocity;
        playerVelocity.y = -slideSpeed;
        _rigidBody.velocity = playerVelocity;
    }

}
