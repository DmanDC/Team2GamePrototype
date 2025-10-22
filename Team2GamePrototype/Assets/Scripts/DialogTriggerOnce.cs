using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerOnce : MonoBehaviour
{
    [TextArea] public string[] sentences;   // The dialog lines for this hotspot
    public DialogManager dialogManager;     // Drag your DialogManager (on your Canvas) here

    private bool triggered = false;         // Prevent repeats in THIS scene run only

    private void Reset()
    {
        // If this object already has a collider, make it a trigger for convenience
        var c = GetComponent<Collider2D>();
        if (c) c.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1) Don't re-run in the same scene
        if (triggered) return;

        // 2) Treat anything with PlayerHealth or Movement as "the player" (works for clone too)
        bool isPlayerLike =
            other.GetComponent<PlayerHealth>() != null ||
            other.GetComponent<Movement>() != null;

        if (!isPlayerLike) return; // Ignore non-player things

        // 3) Find the DialogManager if you didn't drag it in
        if (!dialogManager)
            dialogManager = FindObjectOfType<DialogManager>(true);

        if (!dialogManager)
        {
            Debug.LogError("DialogTriggerOnce: No DialogManager found in the scene.");
            return;
        }

        // 4) Hand off the text and show the panel (DialogManager.OnEnable() will start typing)
        dialogManager.sentences = sentences;
        dialogManager.dialogKey = name;            // uses this GameObject's name as an ID
        if (dialogManager.DialogPanel != null)
            dialogManager.DialogPanel.SetActive(true);
        else
            dialogManager.gameObject.SetActive(true);

        // 5) Lock this hotspot for the rest of THIS scene
        triggered = true;
    }
}
