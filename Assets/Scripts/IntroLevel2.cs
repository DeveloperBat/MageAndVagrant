using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevel2 : MonoBehaviour {

    DialogueSystem ds;
    GameObject vagrant;
    GameObject mage;

	void Start()
    {
        ds = GameObject.Find("Dialogue Canvas").GetComponent<DialogueSystem>();
        vagrant = GameObject.Find("Vagrant");
        mage = GameObject.Find("Mage");
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        ds.PrintDialogue("Tutorial02_01_storage");
        yield return new WaitUntil(() => !ds.IsWriting());
    }
}