using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackSingle : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public float minSize;
    public float maxSize;
    public float minXPosition;
    public float minYPosition;
    //public float maxYPosition;
    public float boundsYAdjust;
    public float boundsXAdjust;
    //public float yDistanceModifier;
    //public float xDistanceModifier;
    public float yDistanceMultiplier;
    public float yDistanceTrigger;
    public float smoothnessMovement;
    public float smoothnessZoom;

    public static bool isFree;
    public static float cameraZPos;

    private float _playerDistance;
    private Camera _gameCamera;
    private Vector3 _velocityMovement = Vector3.zero;
    private float _minYPosition;
    private float _minXPosition;
    public float _maxYPosition;
    private float _vel = 0;

    private void Start()
    {
        isFree = false;
        _gameCamera = GetComponent<Camera>();
        _playerDistance = GetPlayerDistance();
    }

    private void Update()
    {
        if(!isFree)
        {
            _playerDistance = GetPlayerDistance();
            SetCameraSize();
            CenterCamera();
            AdjustBounds();
        }
        cameraZPos = transform.position.z;
    }

    private float GetPlayerDistance()
    {
        float distance = Vector2.Distance(player1.transform.position, player2.transform.position);
        return distance /** xDistanceModifier*/;
    }

    private void SetCameraSize()
    {
        Vector3 cameraPosition = _gameCamera.transform.position;

        if(_playerDistance > minSize && _playerDistance < maxSize)
        {
            if(GetHighestYAxis() - GetLowestYAxis() > yDistanceTrigger)
            {
                cameraPosition.z = -_playerDistance * yDistanceMultiplier;
            }
            else
            {
                cameraPosition.z = -_playerDistance;
            }
        }
        else
        {
            if(_playerDistance <= minSize)
            {
                cameraPosition.z = -minSize;
            }
            else if (_playerDistance >= maxSize)
            {
                cameraPosition.z = -maxSize;
            }
        }
        _gameCamera.transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref _velocityMovement, smoothnessZoom);
    }

    private void CenterCamera()
    {
        Vector3 newCamPosition = (player1.transform.position + player2.transform.position) / 2;
        newCamPosition.z = transform.position.z;
        newCamPosition.y = GetHighestYAxis();

        if(newCamPosition.y < _minYPosition)
        {
            newCamPosition.y = _minYPosition;
        }
        /*else if (newCamPosition.y > _maxYPosition)
        {
            newCamPosition.y = _maxYPosition;
        }*/

        if(newCamPosition.x < _minXPosition)
        {
            newCamPosition.x = _minXPosition;
        }
        
        transform.position = Vector3.SmoothDamp(transform.position, newCamPosition, ref _velocityMovement, smoothnessMovement);
    }

    private void AdjustBounds()
    {
        _minYPosition = minYPosition + -transform.position.z / boundsYAdjust;
        _minXPosition = minXPosition + -transform.position.z / boundsXAdjust;
        //_maxYPosition = maxYPosition + transform.position.z / boundsYAdjust;
    }

    private float GetHighestYAxis()
    {
        float player1YPos = player1.transform.position.y;
        float player2YPos = player2.transform.position.y;

        if(player1YPos < player2YPos /*&& player2YPos - player1YPos > yDistanceModifier*/)
        {
            return player2YPos;
        }
        else
        {
            return player1YPos;
        }
    }

    private float GetLowestYAxis()
    {
        float player1YPos = player1.transform.position.y;
        float player2YPos = player2.transform.position.y;

        if (player1YPos < player2YPos /*&& player2YPos - player1YPos > yDistanceModifier*/)
        {
            return player1YPos;
        }
        else
        {
            return player2YPos;
        }
    }

}
