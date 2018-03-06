using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Rumble : MonoBehaviour {

    //public bool isConnected;

    private PlayerIndex _playerIndex;
    //private GamePadState _gamePadState;
    //private GamePadState _prevGamePadState;
    //private bool _rumbleController;
    private float _rumbleTime;
    private float _lIntensity;
    private float _rIntensity;

    //private bool _playerIndexSet = false;

    private void FixedUpdate()
    {
        if(_rumbleTime > 0)
        {
            GamePad.SetVibration(_playerIndex, _lIntensity, _rIntensity);
            _rumbleTime -= Time.deltaTime;
        }
        else
        {
            GamePad.SetVibration(_playerIndex, 0, 0);
        }
    }

    private void Update()
    {
        /*if(!_playerIndexSet || !_prevGamePadState.IsConnected)
        {
            for(int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    _playerIndex = testPlayerIndex;
                    _playerIndexSet = true;
                }
            }
        }*/

        //_prevGamePadState = _gamePadState;
        //_gamePadState = GamePad.GetState(_playerIndex);

        //isConnected = _gamePadState.IsConnected;
    }

    public void CallRumble(int playerIndex, float lIntensity, float rIntensity, float timer)
    {
        _playerIndex = (PlayerIndex)playerIndex;
        _lIntensity = lIntensity;
        _rIntensity = rIntensity;
        _rumbleTime = timer;
        //_rumbleController = true;
    }

}
