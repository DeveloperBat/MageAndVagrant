using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RotateControllers))]
public class Shoot : MonoBehaviour {

    #region Public Variables
    public GameObject[] projectilePrefabs;
    #endregion

    #region Private Variables

    private RotateControllers controller;
    private Color32 fireColor;
    private Color32 windColor;

    private float rotationOffsetZ;
    private int projectileIndex;
    private bool shooting;

    [Header("Shoot")]
    [SerializeField]
    private float shootDuration;
    [Header("UI")]
    [SerializeField]
    private byte alphaValue;
    [SerializeField]
    private Vector3 scaleValue;
    
    #endregion

    void Start ()
    {
        controller = gameObject.GetComponent<RotateControllers>();

        // Start with fireball preset \\
        rotationOffsetZ = 90f;
        projectileIndex = 0;
        GameObject.Find("UI Firespell").GetComponent<RectTransform>().localScale = scaleValue;
        GameObject.Find("UI Windspell").GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        fireColor = GameObject.Find("UI Firespell").GetComponent<Image>().color;
        windColor = GameObject.Find("UI Windspell").GetComponent<Image>().color;
        fireColor.a = 255;
        windColor.a = alphaValue;
        GameObject.Find("UI Firespell").GetComponent<Image>().color = fireColor;
        GameObject.Find("UI Windspell").GetComponent<Image>().color = windColor;
        // Start with fireball preset \\
    }

    void Update ()
    {
        SelectProjectile();
        if (controller.canShoot == true && shooting == false)
        {
            if(Input.GetButton(FindCharacter.FindPlayer(gameObject) + "Aim"))
            {
                controller.aimingMouse = true;
                if (Input.GetAxis(FindCharacter.FindPlayer(gameObject) + "Shoot") < -0.5)
                {
                    StartCoroutine(ShootProjectile(SelectProjectile()));
                }

            }
            else
            {
                controller.aimingMouse = false;
            }

        }

        
 
    }

    public IEnumerator ShootProjectile(int elementNr)
    {
            shooting = true;
            GameObject projectile = Instantiate(projectilePrefabs[elementNr], transform.position, Quaternion.Euler(0,0, controller.rotation_z + rotationOffsetZ));
            projectile.GetComponent<ProjectileMovement>().Direction(new Vector3(controller.difference.x, controller.difference.y, controller.difference.z));

            yield return new WaitForSeconds(shootDuration);
            shooting = false;
                
    }

    public int SelectProjectile()
    {
        var Wheel = FindCharacter.FindPlayer(gameObject) + "Projectile selection";
        var Button = FindCharacter.FindPlayer(gameObject) + "JoyProjectile selection";

        if (Input.GetButtonDown(Button) && projectileIndex != 0 || Input.GetAxis(Wheel) > 0f && projectileIndex != 0)
        {
            rotationOffsetZ = 90f;
            projectileIndex = 0;

            GameObject.Find("UI Firespell").GetComponent<RectTransform>().localScale = scaleValue;
            GameObject.Find("UI Windspell").GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            fireColor = GameObject.Find("UI Firespell").GetComponent<Image>().color;
            windColor = GameObject.Find("UI Windspell").GetComponent<Image>().color;

            fireColor.a = 255;
            windColor.a = alphaValue;

            GameObject.Find("UI Firespell").GetComponent<Image>().color = fireColor;
            GameObject.Find("UI Windspell").GetComponent<Image>().color = windColor;



        }

        else if (Input.GetButtonDown(Button) && projectileIndex != 1 || Input.GetAxis(Wheel) < 0f && projectileIndex != 1)
        {
            rotationOffsetZ = 90f;
            projectileIndex = 1;

            GameObject.Find("UI Windspell").GetComponent<RectTransform>().localScale = scaleValue;
            GameObject.Find("UI Firespell").GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            fireColor = GameObject.Find("UI Firespell").GetComponent<Image>().color;
            windColor = GameObject.Find("UI Windspell").GetComponent<Image>().color;


            fireColor.a = alphaValue;
            windColor.a = 255;
            
            GameObject.Find("UI Windspell").GetComponent<Image>().color = windColor;
            GameObject.Find("UI Firespell").GetComponent<Image>().color = fireColor;

        }
        return projectileIndex;
    }
}
