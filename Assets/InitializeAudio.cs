using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAudio : MonoBehaviour {


    void Start () {

        FMODUnity.RuntimeManager.PlayOneShot("event:/World/Ambience/ambience_dungeon", new Vector3(0,0,0));

    }


}
