using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneTransition : MonoBehaviour {

    public int sceneIndex;
    public GameObject endGate;

    private Collider2D _collider;
    private int triggerCount;
    private Gate _gate;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _gate = endGate.GetComponent<Gate>();
    }

    private void Update()
    {
        if(triggerCount == 2)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        if(_gate != null)
        {
            _collider.enabled = _gate.isOpen;
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
