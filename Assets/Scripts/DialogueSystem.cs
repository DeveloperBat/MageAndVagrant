using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Put this script on a Canvas GameObject.
public class DialogueSystem : MonoBehaviour {
    public string fileName;
    public string dialogue;

    CanvasGroup canvasDialogue;
    Image imageFrame;
    Animator animatorDialogue;
    TextMeshProUGUI textDialogue;


    private static string staticFileName;

    public class Line
    {
        public string Text { get; private set; }
        public string Character { get; private set; }
        public string Image { get; private set; }
        public Color TextColor { get; private set; }
        public float TextSpeed { get; private set; }
        public float TextWait { get; private set; }

        public Line()
        {
            Text = "This is a placeholder line.";
            Character = "Mage";
            Image = "Default";
            TextColor = new Color(1.0f, 0.0f, 1.0f);
        }
        public Line(string tempText, string tempCharacter, string tempImage, Color tempColor, float tempSpeed, float tempWait)
        {
            Text = tempText;
            Character = tempCharacter;
            Image = tempImage;
            TextColor = tempColor;
            TextSpeed = tempSpeed;
            TextWait = tempWait;
        }
    };

    void Awake()
    {
        staticFileName = fileName;

        canvasDialogue = gameObject.GetComponent<CanvasGroup>();
        imageFrame = gameObject.GetComponentInChildren<Image>();
        animatorDialogue = gameObject.GetComponentInChildren<Animator>();
        textDialogue = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        textDialogue.text = "";
        PrintDialogue(dialogue);
    }

    public void PrintDialogue(string keyword)
    {
        List<string> preLines = GetDialogue(keyword);
        List<Line> lines = GetLines(preLines);
        StartCoroutine(PrintLines(lines));
    }

    private List<string> GetDialogue(string input)
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
            if (line == "" || line.StartsWith("//"))
            {
                continue;
            }
            else if (line.StartsWith("("))
            {
                lines.Add(line);
            }
            else
            {
                break;
            }
        }

        return lines;
    }

    private List<Line> GetLines(List<string> stringLines)
    {
        List<Line> lines = new List<Line>();

        for (int i = 0; i < stringLines.Count; i++)
        {
            //Get all variables in the text.
            string tempLine = stringLines[i];

            Regex rgx = new Regex(@"\(([^)]+)\)");
            MatchCollection matches = rgx.Matches(tempLine);
            List<string> matchList = new List<string>();

            //Check how many matches and put them all in a list of strings.
            foreach (Match match in matches)
            {
                matchList.Add(match.Groups[1].Value);
            }

            //Do something, depending on the number of matches.
            if (matchList.Count == 5)
            {
                //Get RGB Color Information
                rgx = new Regex(@"\d\d\d|\d\d|\d");
                MatchCollection matchesRGB = rgx.Matches(matchList[2]);
                List<string> matchRGBList = new List<string>();

                foreach (Match matchRGB in matchesRGB)
                {
                    matchRGBList.Add(matchRGB.Value);
                }

                float tempRed = Convert.ToSingle(matchRGBList[0]) / 255;
                float tempGreen = Convert.ToSingle(matchRGBList[1]) / 255;
                float tempBlue = Convert.ToSingle(matchRGBList[2]) / 255;

                Color tempColor = new Color(tempRed, tempGreen, tempBlue, 1.0f);

                //Remove all variable parentheses from the text.
                tempLine = Regex.Replace(tempLine, @"\(([^)]+)\)", "");

                //Replace "\n" with '\n'.
                tempLine = tempLine.Replace("\\n", Environment.NewLine);

                lines.Add(new Line(tempLine, matchList[0], matchList[1], tempColor, Convert.ToSingle(matchList[3]), Convert.ToSingle(matchList[4])));
            }
            else
            {
                Debug.LogAssertion("There are no matches in this line! Make sure the line contains identifiers by using paranthesis! stringLines[i]: " + stringLines[i]);
                lines.Add(new Line("THIS IS AN ERROR LINE. REFER TO THE DEBUG LOG IN-GAME.", "Mage", "Default", new Color(1.0f, 0.0f, 0.0f), 0.0f, 3.0f));
            }
        }
        return lines;
    }

    private IEnumerator PrintLines(List<Line> lines)
    {
        yield return StartCoroutine(FadeDialogue(1f, 0.5f));
        for (int i = 0; i < lines.Count; i++)
        {
            yield return StartCoroutine(PrintLine(lines[i]));
        }
        yield return StartCoroutine(FadeDialogue(0f, 2f));
    }

    private IEnumerator PrintLine(Line currentLine)
    {
        animatorDialogue.Play(currentLine.Character + currentLine.Image);
        textDialogue.color = currentLine.TextColor;

        yield return StartCoroutine(AnimateText(currentLine));
    }

    private IEnumerator AnimateText(Line currentLine)
    {
        Regex rgx = new Regex(@"(<[^>]+>|\n)");
        MatchCollection matches = rgx.Matches(currentLine.Text);

        int i = 0;
        while (i < currentLine.Text.Length)
        {
            if (currentLine.Text[i] == '<')
            {
                foreach (Match match in matches)
                {

                    if (match.Groups[0].Index == i)
                    {
                        i = i + match.Groups[0].Length;
                        textDialogue.text = currentLine.Text.Substring(0, i);
                        PlaySound();
                        yield return new WaitForSeconds(currentLine.TextSpeed);
                    }
                }
            }
            else if (currentLine.Text[i] == '\r' || currentLine.Text[i] == '\n' || currentLine.Text[i] == ' ')
            {
                i++;
                yield return null;
            }
            else
            {
                textDialogue.text = currentLine.Text.Substring(0, i + 1);
                i++;
                yield return new WaitForSeconds(currentLine.TextSpeed);
            }
        }
        yield return new WaitForSeconds(currentLine.TextWait);
    }

    private IEnumerator FadeDialogue(float targetAlpha, float duration)
    {
        float currentAlpha = canvasDialogue.alpha;
        float currentTime = 0.0f;
        while (currentTime < duration)
        {
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, currentTime / duration);
            canvasDialogue.alpha = newAlpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private void PlaySound()
    {
    }

    private void PlayCharacterSound(string character, string type)
    {
    }
}