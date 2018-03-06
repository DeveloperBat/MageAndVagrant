using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

    public delegate void DisableAllCheckpoints();
    public static event DisableAllCheckpoints OnCheckpointDisable;

    public static GameObject activeCheckpoint;
    public static List<GameObject> checkPoints;

    public List<GameObject> levelCheckpoints;

    private void Start()
    {
        checkPoints = levelCheckpoints;
        activeCheckpoint = checkPoints[PlayerPrefs.GetInt("CP", 0)];
    }

    public static void DisableCheckpoints()
    {
        OnCheckpointDisable();
    }

    public static void LoadCheckpoint(int loadedIndex)
    {
        DisableCheckpoints();
        activeCheckpoint = checkPoints[loadedIndex];
        Debug.Log(loadedIndex);
    }

    /*public List<GameObject> checkPoints;
    public List<Checkpoint> _cpScripts;

    private void Start()
    {
        GetScripts();
    }

    private void GetScripts()
    {
        if(checkPoints.Count > 0)
        {
            for (int i = 0; i < checkPoints.Count; i++)
            {
                _cpScripts.Insert(0, checkPoints[i].GetComponent<Checkpoint>());
            }
        }
    }

    public void DisableCheckpoints()
    {
        if(_cpScripts.Count > 0)
        {
            for (int i = 0; i < _cpScripts.Count; i++)
            {
                _cpScripts[i].isActive = false;
            }
        }
    }*/

}
