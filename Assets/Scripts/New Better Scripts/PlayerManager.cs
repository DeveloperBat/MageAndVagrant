using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public delegate void PlayerRespawn();
    public static event PlayerRespawn OnPlayerRespawn;

    public GameObject player1;
    public GameObject player2;
    public float deathDelay;
    public float respawnDelay;

    private bool _respawnPlayers;
    private GameObject _activeCP;

    private void Start()
    {
        Player.OnPlayerDead += KillPlayers;
    }

    private void OnApplicationQuit()
    {
        Player.OnPlayerDead -= KillPlayers;
    }

    private void Update()
    {
        if(_respawnPlayers)
        {
            OnPlayerRespawn();
            RespawnPlayers();
            _respawnPlayers = false;
        }

        _activeCP = CheckPointManager.activeCheckpoint;
    }

    private void KillPlayers()
    {
        /*player1.SetActive(false);
        player2.SetActive(false);*/
        _respawnPlayers = true;
    }

    private void RespawnPlayers()
    {
        Vector3 cpPosition = _activeCP.GetComponent<RectTransform>().position;
        player1.GetComponent<RectTransform>().transform.position = cpPosition;
        player2.GetComponent<RectTransform>().transform.position = cpPosition;
    }

    /*public GameObject player1;
    public GameObject player2;
    public GameObject checkPoint;
    public float deathDelay;
    public float respawnDelay;

    private Player _player1Script;
    private Player _player2Script;
    private RectTransform _p1Transform;
    private RectTransform _p2Transform;
    private RectTransform _cpTransform;
    private float _deathTimeLeft;
    private float _respawnTimeLeft;

    private void Start()
    {
        _player1Script = player1.GetComponent<Player>();
        _player2Script = player2.GetComponent<Player>();
        _p1Transform = player1.GetComponent<RectTransform>();
        _p2Transform = player2.GetComponent<RectTransform>();
        _deathTimeLeft = deathDelay;
        _respawnTimeLeft = respawnDelay;
    }

    private void Update()
    {
        KillPlayers();
        KillOtherPlayer();
        RespawnPlayers();
        _cpTransform = checkPoint.GetComponent<RectTransform>();
    }

    private void KillPlayers()
    {
        if(_player1Script.isDead == true)
        {
            player1.SetActive(false);
        }

        if(_player2Script.isDead == true)
        {
            player2.SetActive(false);
        }
    }

    private void KillOtherPlayer()
    {
        if(_player1Script.isDead || _player2Script.isDead)
        {
            _deathTimeLeft -= Time.deltaTime;
            if(_deathTimeLeft < 0)
            {
                _player1Script.isDead = true;
                _player2Script.isDead = true;
            }
        }
        else
        {
            _deathTimeLeft = deathDelay;
        }
    }

    private void RespawnPlayers()
    {
        if(_player1Script.isDead && _player2Script.isDead)
        {
            _respawnTimeLeft -= Time.deltaTime;
            if(_respawnTimeLeft < 0)
            {
                player1.SetActive(true);
                player2.SetActive(true);
                _p1Transform.position = _cpTransform.position;
                _p2Transform.position = _cpTransform.position;
                _player1Script.isDead = false;
                _player2Script.isDead = false;
            }
        }
        else
        {
            _respawnTimeLeft = respawnDelay;
        }
    }*/

}
