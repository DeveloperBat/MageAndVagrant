using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    public Camera gameCamera;
    public float sceneSize;
    public Vector3 scenePosition;
    public float zoomSmoothness;
    public float moveSmoothness;

    private bool _isTriggered;
    //private float _velocity = 0;
    private Vector3 _velocityMove = Vector3.zero;

    private void Start()
    {
        _isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _isTriggered = true;
            CameraTrackSingle.isFree = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _isTriggered = false;
            CameraTrackSingle.isFree = false;
        }
    }

    private void Update()
    {
        if(_isTriggered)
        {
            SetScenePosition();
        }
    }

    private void SetScenePosition()
    {
        Vector3 newCamPos = scenePosition;
        //newCamPos.z = gameCamera.transform.position.z;
        newCamPos.z = sceneSize;

        //gameCamera.transform.position = newCamPos;
        gameCamera.transform.position = Vector3.SmoothDamp(gameCamera.transform.position, newCamPos, ref _velocityMove, moveSmoothness);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 lineFrom = transform.position;

        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        Gizmos.DrawLine(lineFrom, scenePosition);
    }

}
