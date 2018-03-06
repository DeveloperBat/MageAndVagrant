using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [SerializeField]
    private float angleLimit_1;
    [SerializeField]
    private float angleLimit_2;

    private float eulerValue = 150;
    
    

    void Update ()
    {
        rotateShield();
        
    }

    void rotateShield()
    {
        if (Input.GetKey("w"))
        {
            transform.Rotate(new Vector3(0,0, eulerValue * Time.deltaTime));  
        }

        
        if (Input.GetKey("s"))
        {
            transform.Rotate(new Vector3(0, 0, -eulerValue * Time.deltaTime));
        }

        
       if (transform.eulerAngles.z >= angleLimit_1 && transform.eulerAngles.z < 270)
        {
            transform.eulerAngles = new Vector3(0, 0, angleLimit_1);
        }


        if (transform.eulerAngles.z <= angleLimit_2 && transform.eulerAngles.z > 270)
        {
            transform.eulerAngles = new Vector3(0, 0, angleLimit_2);
        }


    }
}
    