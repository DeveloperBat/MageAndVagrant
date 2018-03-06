using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(ManualMovement))]
public class Player : MonoBehaviour {

    public static bool isAlive = true;
    public delegate void PlayerDie();
    public static event PlayerDie OnPlayerDead;

    public List<string> deadlyObjects;
    public List<string> interactables;

    private GameObject _inRangeObj;
    private bool _interactAxisPressed;

    private void Start()
    {
        PlayerManager.OnPlayerRespawn += Respawn;
    }

    private void Awake()
    {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }

    private void Update()
    {
        InteractWithObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(deadlyObjects.Contains(collision.gameObject.tag) && isAlive)
        {
            OnPlayerDead();
            isAlive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactables.Contains(collision.gameObject.tag))
        {
            _inRangeObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _inRangeObj)
        {
            _inRangeObj = null;
        }
    }

    private void InteractWithObject()
    {
        if (Input.GetAxisRaw(FindCharacter.FindPlayer(gameObject) + "Interact") > 0 && _inRangeObj != null && !_interactAxisPressed)
        {
            _inRangeObj.GetComponent<Interactable>().ToggleInteractive();
            _interactAxisPressed = true;
        }
        else if (Input.GetAxisRaw(FindCharacter.FindPlayer(gameObject) + "Interact") == 0 && _interactAxisPressed)
        {
            _interactAxisPressed = false;
        }
    }

    private void Respawn()
    {
        isAlive = true;
    }

    /*public bool isDead;
    public List<string> deadlyObjects;
    public List<string> interactables;

    private GameObject _inRangeObj;
    private ManualMovement.Player _player;
    public bool _interactAxisPressed;
    private string _pAxis;

    void Awake()
    {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }

    private void Start()
    {
        _player = gameObject.GetComponent<ManualMovement>().player;
        if(_player == ManualMovement.Player.Player1)
        {
            _pAxis = "P1";
        }
        else if (_player == ManualMovement.Player.Player2)
        {
            _pAxis = "P2";
        }
    }

    private void Update()
    {
        InteractWithObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(deadlyObjects.Contains(collision.gameObject.tag))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(interactables.Contains(collision.gameObject.tag))
        {
            _inRangeObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(interactables.Contains(collision.gameObject.tag))
        {
            _inRangeObj = null;
        }
    }

    private void InteractWithObject()
    {
        if(Input.GetAxisRaw(_pAxis + "Interact") > 0 && _inRangeObj != null && !_interactAxisPressed)
        {
            _inRangeObj.GetComponent<Interactable>().ToggleInteractive();
            _interactAxisPressed = true;
        }
        else if (Input.GetAxisRaw(_pAxis + "Interact") == 0 && _interactAxisPressed)
        {
            _interactAxisPressed = false;
        }
    }

    private void Die()
    {
        isDead = true;
    }
    */
}
