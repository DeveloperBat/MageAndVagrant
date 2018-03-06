using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCharacter : MonoBehaviour {

    public static string FindPlayer(GameObject player)
    {
        string playerName = "P1";

       if(player.name == "Mage")
        {
            playerName = "P1";
        }
       else if(player.name == "Vagrant")
        {
            playerName = "P2";
        }

        return playerName;
    }

}
