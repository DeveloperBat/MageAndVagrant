using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FindCharacter))]
[RequireComponent(typeof(Rigidbody2D))]
[HelpURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
public class Blink : MonoBehaviour {

    #region Private Variables
    private RaycastHit2D blinkRay;
    private Animator animator;
    private Vector3 blink;
    private Rigidbody2D rb2d;

    private float xDir; 
    private bool blinking;
    private bool floating;

    [Header("Blink")]
    [SerializeField]
    private float blinkDuration;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float degrees;

    [Header("Float")]
    [SerializeField]
    private float dragValue;
    [SerializeField]
    private float floatDuration;
    #endregion

    LayerMask mask = ~(1 << 8);

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        blinking = false;
        floating = false;

	}
	
	void Update ()
    {
        
        HandleRay();

        if (animator.GetBool("facingRight") == true)
        {
            xDir = 1;
        }
        else
        {
            xDir = -1;
        }

        if (Input.GetButtonDown(FindCharacter.FindPlayer(gameObject) + "Blink") && blinking == false && floating == false)
        {
            StartCoroutine(BlinkAway());
            StartCoroutine(FloatAway());
   
        }



    }

    public void HandleRay()
    {

        blink = Quaternion.AngleAxis(degrees, Vector3.forward) * Vector3.right * distance;

        blink.x = blink.x * xDir;

        blinkRay = Physics2D.Raycast(transform.position, blink, distance, mask);

        if (blinkRay.collider != null)
        {
            Debug.Log("Nuddar: " +  blinkRay.collider.name);
            if (blinkRay.collider.tag == "Wall") // Kan ändras beroende på vad man ska blinka igenom.
            {
                float newvalueX = blinkRay.point.x - transform.position.x;
                float newvalueY = blinkRay.point.y - transform.position.y;

                blink = new Vector3(newvalueX, newvalueY, 0);
                Debug.DrawRay(transform.position, blink, Color.red);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, blink, Color.green); 

        }

    }

    public IEnumerator BlinkAway()
    {
        blinking = true;
        transform.position += blink; 
        yield return new WaitForSeconds(blinkDuration);
        blinking = false;
        
    }

    public IEnumerator FloatAway()
    {

        floating = true;
        rb2d.drag = dragValue;
        yield return new WaitForSeconds(floatDuration);
        rb2d.drag = 0;
        floating = false;

    }


}
