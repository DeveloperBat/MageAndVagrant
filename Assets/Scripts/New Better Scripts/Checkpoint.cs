using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Checkpoint : MonoBehaviour {

    public bool isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isActive)
        {
            CheckPointManager.DisableCheckpoints();
            isActive = true;
            CheckPointManager.activeCheckpoint = gameObject;
        }
    }

    private void Start()
    {
        CheckPointManager.OnCheckpointDisable += Deactivate;
    }

    private void Deactivate()
    {
        isActive = false;
    }

    /*public bool isActive;
    public GameObject playerManager;
    public GameObject checkPointManager;

    private PlayerManager _pManager;
    private CheckPointManager _cpManager;

    private void OnEnable()
    {
        _pManager = playerManager.GetComponent<PlayerManager>();
        _cpManager = checkPointManager.GetComponent<CheckPointManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _pManager.checkPoint = this.gameObject;
            _cpManager.DisableCheckpoints();
            isActive = true;
        }
    }*/

}
