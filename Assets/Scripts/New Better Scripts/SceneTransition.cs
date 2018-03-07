using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneTransition : MonoBehaviour {

    public int sceneIndex;

    private Collider2D _collider;
    private int triggerCount;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void Update()
    {
        if(triggerCount == 2)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            triggerCount += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerCount -= 1;
        }
    }

}
