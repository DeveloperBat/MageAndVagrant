using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FixedMovement : MonoBehaviour {

    public float moveSpeedX;
    public float moveSpeedY;

    public float moveRangeX;
    public float moveRangeY;

    public float rangeAdjustX;
    public float rangeAdjustY;

    private float _maxXPosition;
    private float _minXPosition;

    private float _maxYPosition;
    private float _minYPosition;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        SetRange();
    }

    private void Update()
    {
        Move();
        ChangeDirection();
    }

    private void SetRange()
    {
        _maxXPosition = transform.position.x + moveRangeX + rangeAdjustX;
        _minXPosition = transform.position.x - moveRangeX + rangeAdjustX;

        _maxYPosition = transform.position.y + moveRangeY + rangeAdjustY;
        _minYPosition = transform.position.y - moveRangeY + rangeAdjustY;
    }

    private void Move()
    {
        Vector3 rbVelocity = _rigidBody.velocity;

        rbVelocity.x = moveSpeedX;
        rbVelocity.y = moveSpeedY;

        _rigidBody.velocity = rbVelocity;
    }

    private void ChangeDirection()
    {
        float xPosition = transform.position.x;
        float yPosition = transform.position.y;

        if(moveRangeX > 0)
        {
            if(xPosition > _maxXPosition && moveSpeedX > 0)
            {
                moveSpeedX *= -1;
            }
            else if (xPosition < _minXPosition && moveSpeedX < 0)
            {
                moveSpeedX *= -1;
            }
        }

        if(moveRangeY > 0)
        {
            if(yPosition > _maxYPosition && moveSpeedY > 0)
            {
                moveSpeedY *= -1;
            }
            else if (yPosition < _minYPosition && moveSpeedY < 0)
            {
                moveSpeedY *= -1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector2 speedLineFrom = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.2f);
        Vector2 rangeLineFrom = new Vector2(transform.position.x - moveRangeX + rangeAdjustX, transform.position.y - moveRangeY + rangeAdjustY);

        Vector2 speedLineTo = new Vector2(transform.position.x + moveSpeedX + 0.2f, transform.position.x + moveSpeedY + 0.2f);
        Vector2 rangeLineTo = new Vector2(transform.position.x + moveRangeX + rangeAdjustX, transform.position.y + moveRangeY + rangeAdjustY);

        Gizmos.DrawLine(speedLineFrom, speedLineTo);
        Gizmos.DrawLine(rangeLineFrom, rangeLineTo);
    }

}
