using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShield : MonoBehaviour {

    private bool isActive;
    private GameObject shield;

    private void Start()
    {
        isActive = false;
        shield = GameObject.FindGameObjectWithTag("Shield");
    }

    void Update () {
		if(Input.GetButtonDown(FindCharacter.FindPlayer(gameObject) + "ToggleShield"))
        {

            isActive = !isActive;

            if (isActive)
            {
                shield.SetActive(true);
            }
            else
            {
                shield.SetActive(false);
            }

        }


    }
}
