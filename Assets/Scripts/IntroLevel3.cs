using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLevel3 : MonoBehaviour {

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
        SetMovement(false);
        ds.PrintDialogue("Tutorial01_01_intro");
        yield return new WaitUntil(() => !ds.IsWriting());
        SetMovement(true);
    }

    void SetMovement(bool trueOrFalse)
    {
        vagrant.GetComponent<ManualMovement>().enabled = trueOrFalse;
        vagrant.GetComponentInChildren<Shield>().enabled = trueOrFalse;

        mage.GetComponent<ManualMovement>().enabled = trueOrFalse;
        mage.GetComponentInChildren<Shoot>().enabled = trueOrFalse;
    }
}