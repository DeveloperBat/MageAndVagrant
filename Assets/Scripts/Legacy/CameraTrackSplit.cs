using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackSplit : MonoBehaviour {

    public GameObject playerTracked;
    public GameObject otherPlayer;
    public enum Following
    {
        Player1,
        Player2
    }
    public Following following;
    public float cameraSize;

    private Camera _splitCamera;
    private bool _isRightSide;

    private void Start()
    {
        _splitCamera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        SetCameraPosition();
        SetCameraSize();
        SetViewPortPosition();
        SetViewPort();
    }

    private void SetCameraPosition()
    {
        Vector3 newCamPosition = playerTracked.transform.position;

        newCamPosition.z = transform.position.z;

        transform.position = newCamPosition;
    }

    private void SetViewPort()
    {
        switch (following)
        {
            case Following.Player1:
                if (_isRightSide == false)
                {
                    _splitCamera.rect = new Rect(0, 0, 0.5f, 1);
                }
                else
                {
                    _splitCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
                }
                break;
            case Following.Player2:
                if (_isRightSide == false)
                {
                    _splitCamera.rect = new Rect(0, 0, 0.5f, 1);
                }
                else
                {
                    _splitCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
                }
                break;
        }
    }

    private void SetViewPortPosition()
    {
        if(playerTracked.transform.position.x > otherPlayer.transform.position.x)
        {
            _isRightSide = true;
        }
        else if(playerTracked.transform.position.x < otherPlayer.transform.position.x)
        {
            _isRightSide = false;
        }
    }

    private void SetCameraSize()
    {
        _splitCamera.fieldOfView = cameraSize;
    }

}
