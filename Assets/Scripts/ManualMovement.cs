using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ManualMovement : MonoBehaviour {

    public float movementSpeed;
    public float maxMovementSpeed;
    public float acceleration;
    public float deacceleration;
    public float jumpForce;
    public float fallGravity;
    public float violentVelocity;
    public enum Player
    {
        Player1,
        Player2
    }
    public Player player;
    public List<string> jumpObj;
    public bool canJump;
    public bool isGrounded;
    public bool jumped;
    public Vector2 playerPosition;
    public RectTransform rectTransform;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private CameraShake _cameraShake;
    private Rumble _rumble;
    private string _axisNameHorizontal;
    private string _axisNameVertical;
    private string _axisNameJump;
    private float _rbGravity;
    private int _playerIndex;
    private bool _jumpPressed;
    private bool _jumping;
    public GameObject _footing;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _cameraShake = gameObject.GetComponent<CameraShake>();
        _rumble = gameObject.GetComponent<Rumble>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        SetAxisNames();
        SetPlayerIndex();
        _rbGravity = _rigidBody.gravityScale;
        //jumpObj = new List<string>() { "Player", "Ground", "Fire Projectile", "Platform"};
    }

    private void Update()
    {
        SetAnimatorVar();

    }

    private void FixedUpdate()
    {
        CharJump();
        CharMove();
        CharFall();
        SetJumpAnimationVariables();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumpObj.Contains(collision.gameObject.tag) && _footing == null)
        {
            canJump = true;
            jumped = false;
            isGrounded = true;
            _footing = collision.gameObject;
            //_animator.SetBool("jumping", false);
            if(collision.relativeVelocity.y > violentVelocity)
            {
                //_cameraShake.UseCameraShake(0.2f, 2, 0);
                _rumble.CallRumble(_playerIndex, 0.5f, 0.5f, 0.2f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == _footing)
        {
            isGrounded = false;
            _footing = null;
            canJump = false;
            //_animator.SetBool("jumping", true);
        }
    }

    private void SetPlayerIndex()
    {
        switch (player)
        {
            case Player.Player1:
                _playerIndex = 0;
                break;
            case Player.Player2:
                _playerIndex = 1;
                break;
        }
    }

    private void CharJump()
    {
        if (Input.GetAxis("P2Vertical") > 0)
        {
            if (canJump && !_jumping)
            {
                Vector3 rbVelocity = _rigidBody.velocity;
                rbVelocity.y = jumpForce;
                _rigidBody.velocity = rbVelocity;
                canJump = false;
                jumped = true;
                _jumping = true;
            }
        }
        else if(Input.GetAxis("P2Vertical") == 0)
        {
            _jumping = false;
        }
    }

    private void CharMove()
    {
        //Acceleration Code
        if (Input.GetAxis(_axisNameHorizontal) < 0)
        {
            movementSpeed = movementSpeed - acceleration * Time.deltaTime;
        }
        else if (Input.GetAxis(_axisNameHorizontal) > 0)
        {
            movementSpeed = movementSpeed + acceleration * Time.deltaTime;
        }
        else
        {
            if (movementSpeed > deacceleration * Time.deltaTime)
            {
                movementSpeed = movementSpeed - deacceleration * Time.deltaTime;
            }
            else if (movementSpeed < -deacceleration * Time.deltaTime)
            {
                movementSpeed = movementSpeed + deacceleration * Time.deltaTime;
            }
            else
            {
                movementSpeed = 0;
            }
        }

        transform.position = new Vector3(transform.position.x + movementSpeed, _rigidBody.transform.position.y, _rigidBody.transform.position.z);

        /*if(Input.GetAxis(_axisNameHorizontal) > 0)
        {
            Vector3 rbVelocity = _rigidBody.velocity;
            rbVelocity.x = movementSpeed;
            _rigidBody.velocity = rbVelocity;
        }
        else if (Input.GetAxis(_axisNameHorizontal) < 0)
        {
            Vector3 rbVelocity = _rigidBody.velocity;
            rbVelocity.x = -movementSpeed;
            _rigidBody.velocity = rbVelocity;
        }*/
    }

    private void CharFall()
    {
        if(_rigidBody.velocity.y < 0)
        {
            _rigidBody.gravityScale = fallGravity;
        }
        else
        {
            _rigidBody.gravityScale = _rbGravity;
        }
    }

    private void SetAxisNames()
    {
        _axisNameHorizontal = "Horizontal";
        _axisNameVertical = "Vertical";
        _axisNameJump = "Jump";

        switch (player)
        {
            case Player.Player1:
                _axisNameHorizontal = "P1" + _axisNameHorizontal;
                _axisNameVertical = "P1" + _axisNameVertical;
                _axisNameJump = "P1" + _axisNameJump;
                break;
            case Player.Player2:
                _axisNameHorizontal = "P2" + _axisNameHorizontal;
                _axisNameVertical = "P2" + _axisNameVertical;
                _axisNameJump = "P2" + _axisNameJump;
                break;
        }
    }

    private void SetJumpAnimationVariables()
    {
        if(_rigidBody.velocity.y > 0)
        {
            _animator.SetBool("jumping", true);
        }
        else if (_rigidBody.velocity.y < 0)
        {
            _animator.SetBool("jumping", false);
            _animator.SetBool("atHeightPeak", true);
            _animator.SetBool("falling", true);
        }
        else if (isGrounded && _rigidBody.velocity.y == 0)
        {
            _animator.SetBool("falling", false);
            _animator.SetBool("jumping", false);
            _animator.SetBool("atHeightPeak", false);
        }
    }

    private void SetAnimatorVar()
    {
        float horizontalMovement = Input.GetAxis(_axisNameHorizontal);

        if(horizontalMovement > 0)
        {
            _animator.SetBool("facingRight", true);
            _animator.SetBool("isMoving", true);
        }
        else if (horizontalMovement < 0)
        {
            _animator.SetBool("facingRight", false);
            _animator.SetBool("isMoving", true);
        }
        else if(horizontalMovement == 0)
        {
            _animator.SetBool("isMoving", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 lineFrom = transform.position;

        Vector3 speedTo = transform.position;
        speedTo.x += movementSpeed;

        Vector3 jumpTo = transform.position;
        jumpTo.y += jumpForce / 10;

        Gizmos.DrawLine(lineFrom, speedTo);
        Gizmos.DrawLine(lineFrom, jumpTo);
    }

}
