using System;
using System.Collections;
using System.Collections.Generic;
//Must add this using statment to use TMP_Text
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
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
        // Auto-wire if something wasn’t set in the Inspector
        if (!DialogPanel) DialogPanel = gameObject; // or find the panel explicitly
        if (!textbox) textbox = GetComponentInChildren<TMPro.TMP_Text>(true);
        if (!continueButton)
        {
            var btn = GetComponentInChildren<Button>(true); // finds the first Button (even if inactive)
            if (btn) continueButton = btn.gameObject;       // assign its GameObject
        }

        // Final guard: if anything is still missing, bail with a clear message
        if (!DialogPanel || !textbox || !continueButton || sentences == null || sentences.Length == 0)
        {
            Debug.LogError("DialogManager not wired in this scene: assign DialogPanel, textbox, continueButton, and sentences.");
            enabled = false;
            return;
        }

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
