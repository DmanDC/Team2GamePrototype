using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Must add this using statment to use TMP_Text
using TMPro;
using UnityEngine.Analytics;
public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject DialogPanel;

    public string dialogKey = "Intro_01";

   

    // session-memory: resets when the app restarts (and when domain reload happens in Editor)
    private static readonly HashSet<string> shownThisSession = new HashSet<string>();

    private void OnEnable()
    {
        // If we've already shown this dialog in this app session, hide the panel and exit
        if (shownThisSession.Contains(dialogKey))
        {
            if (DialogPanel != null) DialogPanel.SetActive(false);
            return;
        }

        // Make sure the panel is visible before typing
        if (DialogPanel != null) DialogPanel.SetActive(true);

        if (continueButton != null) continueButton.SetActive(false);

        index = 0;
        textbox.text = "";
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        textbox.text = "";

        foreach (char letter in sentences[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (continueButton != null) continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        if (continueButton != null) continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            // finished: mark as shown for this session and hide
            shownThisSession.Add(dialogKey);
            textbox.text = "";
            if (DialogPanel != null) DialogPanel.SetActive(false);
        }
    }

}
