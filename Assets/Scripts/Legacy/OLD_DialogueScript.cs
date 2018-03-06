using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {
    public GameObject mage;
    public GameObject vagrant;
    public string fileName;

    private static string staticFileName;
    private bool isFinished;

    private void Start()
    {
        staticFileName = fileName;
        mage.GetComponentInChildren<Text>().text = "";
        vagrant.GetComponentInChildren<Text>().text = "";

        List<string> lines = GetDialogueLines("Lever");
        StartCoroutine(PrintDialogue(lines));
    }

    private List<string> GetDialogueLines(string input)
    {
        StreamReader sr = new StreamReader(staticFileName);
        List<string> lines = new List<string>();
        string fixedInput = "#" + input;
        string line = sr.ReadLine();

        //Try and search for the given input in the text file.
        while (line != fixedInput)
        {
            if (sr.EndOfStream)
            {
                Debug.LogAssertion("Could not find \"" + input + "\" in the given text file.");
                break;
            }
            line = sr.ReadLine();
        }

        while (!sr.EndOfStream)
        {
            line = sr.ReadLine();
            lines.Add(line);
        }

        return lines;
    }

    private IEnumerator PrintDialogue(List<string> dialogueTree)
    {
        for (int i = 0; i < dialogueTree.Count; i++)
        {
            string tempLine = dialogueTree[i];

            if (tempLine.StartsWith("(M)"))
            {
                tempLine = tempLine.Substring(3);
                StartCoroutine(AnimateText(tempLine, mage, 0.10f));
                yield return new WaitUntil(() => isFinished == true);
                yield return new WaitForSeconds(1.5f);
            }

            else if (tempLine.StartsWith("(V)"))
            {
                tempLine = tempLine.Substring(3);
                StartCoroutine(AnimateText(tempLine, vagrant, 0.10f));
                yield return new WaitUntil(() => isFinished == true);
                yield return new WaitForSeconds(1.5f);
            }

            else
            {
                Debug.LogAssertion("Could not find sayer in string: " + tempLine);
            }
        }
    }

    private IEnumerator AnimateText(string text, GameObject character, float textSpeed)
    {
        isFinished = false;
        for (int i = 0; i < text.Length + 1; i++)
        {
            character.GetComponentInChildren<Text>().text = text.Substring(0, i);
            character.GetComponentInChildren<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            character.GetComponentInChildren<AudioSource>().Play();
            yield return new WaitForSeconds(textSpeed);
        }
        isFinished = true;
    }

    private IEnumerator FadeText(GameObject character, float end, float fadeSpeed)
    {
        yield return 0;
    }
}