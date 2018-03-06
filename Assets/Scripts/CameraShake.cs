using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public delegate void CamShake(float shakeTime, float shakeSize, float shakeSmooth);
    public static event CamShake OnCameraShake;

    private float _shakeTime;
    private float _shakeSize;
    private float _shakeSmooth;

    private bool _shakeCamera;
    private float _startTimer;

    private void Start()
    {
        _startTimer = _shakeTime;
        OnCameraShake += UseCameraShake;
    }

    private Vector3 shakeVelocity = Vector3.zero;

    private bool _isShaking;

    private void Update()
    {
        if(_shakeCamera)
        {
            ShakeCamera();
        }
    }

    private void ShakeCamera()
    {
        if(_shakeTime > 0)
        {
            Vector2 shakePosition = Random.insideUnitCircle * _shakeSize;
            Vector3 newShakePos = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newShakePos, ref shakeVelocity, _shakeSmooth);
            _shakeTime -= Time.deltaTime;
        }
        else
        {
            _shakeCamera = false;
        }
    }

    private void ResetTimer()
    {
        _shakeTime = _startTimer;
    }

    public void UseCameraShake(float shakeTime, float shakeSize, float shakeSmooth)
    {
        _shakeCamera = true;
        _startTimer = shakeTime;
        _shakeSize = shakeSize;
        _shakeSmooth = shakeSmooth;
        ResetTimer();
    }

}
