using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Camera mainCamera;
    public Camera player1Camera;
    public Camera player2Camera;
    public GameObject player1;
    public GameObject player2;
    public float splitPoint;

    private void Start()
    {
        mainCamera.enabled = true;
        player1Camera.enabled = false;
        player2Camera.enabled = false;
    }

    private void Update()
    {
        SplitCamera();
    }

    private void SplitCamera()
    {
        if(GetPlayerDistance() > splitPoint)
        {
            mainCamera.enabled = false;
            player1Camera.enabled = true;
            player2Camera.enabled = true;
        }
        else if (GetPlayerDistance() < splitPoint)
        {
            mainCamera.enabled = true;
            player1Camera.enabled = false;
            player2Camera.enabled = false;
        }
    }

    private float GetPlayerDistance()
    {
        float distance = Vector2.Distance(player1.transform.position, player2.transform.position);
        return distance;
    }

}
