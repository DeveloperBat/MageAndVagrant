using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour {

    #region Private Variables

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 direction;
    private Vector3 startVel;

    [SerializeField]
    private float length;
    [SerializeField]
    private float xValue;
    [SerializeField]
    private float yValue;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float smoothTime;
    #endregion

    void Start ()
    {

        startVel = Vector3.zero;

        startPos = transform.position;

        direction = new Vector3(xValue, yValue, 0);

        endPos = transform.position + direction * length;

    }



    void LateUpdate ()
    {

        transform.position = Vector3.SmoothDamp(startPos, endPos, ref startVel, smoothTime);


        if (startPos.x >= endPos.x || startPos.y >= endPos.y)
        {
            Debug.Log("Längst upp");
        }
        
        Debug.DrawRay(startPos, direction * length, Color.blue);
        
	}
}
