using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControllers : RotateToJoystick{

    public bool externalController;

    public bool aimingMouse;

    void Update()
    {
        if (externalController)
        {
            sRenderer.enabled = false;
            canShoot = false;
            RotateJoystick();

        }
        else
        {
            sRenderer.enabled = false;
            canShoot = true;
            RotateMouse();

        }
    }

    public void RotateMouse()
    {
        if (aimingMouse)
        {
            sRenderer.enabled = true;
            var mousePos = Input.mousePosition;
            var magePos = Camera.main.WorldToScreenPoint(transform.position);

            difference = mousePos - magePos;

            difference.Normalize();
            rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, rotation_z + 90));
        }
        else
        {
            sRenderer.enabled = false;
        }

    }
}
