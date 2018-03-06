using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToJoystick : MonoBehaviour {

    [SerializeField]
    private float dirOffset;

    public SpriteRenderer sRenderer;
    public Vector3 difference;
    public bool canShoot;
    public float rotation_z;



    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        canShoot = false;
        dirOffset = 90; // Anpassad med indicatorn
    }


    void Update()
    {
        RotateJoystick();
    }

    public void RotateJoystick()
    {

        if(Input.GetAxis(FindCharacter.FindPlayer(gameObject) + "AimHorizontal") != 0 || Input.GetAxis(FindCharacter.FindPlayer(gameObject) + "AimVertical") != 0)
        {

            sRenderer.enabled = true;
            canShoot = true;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(Input.GetAxis(FindCharacter.FindPlayer(gameObject) + "AimHorizontal"), Input.GetAxis(FindCharacter.FindPlayer(gameObject) + "AimVertical")) * 180 / Mathf.PI + 180);
            difference = Quaternion.AngleAxis(transform.eulerAngles.z + dirOffset, Vector3.forward) * Vector3.right;
            rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else
        {
            sRenderer.enabled = false;
            canShoot = false;
        }
    }
    
}
