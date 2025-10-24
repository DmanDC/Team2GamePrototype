using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerOnce : MonoBehaviour
{
    [TextArea] public string[] sentences;   // sentences you can add
    public DialogManager dialogManager;     // put dialog manager here
    public bool loadSceneOnFinish = false;
    public string sceneToLoad = "";
    public float loadDelay = 0f;

    private bool triggered = false;         // Prevent repeats in this scene run only

    private void Reset()
    {
        
        var c = GetComponent<Collider2D>();
        if (c) c.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1) Don't re-run in the same scene
        if (triggered) return;

        // treats clones as player
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
        dialogManager.loadSceneOnFinish = loadSceneOnFinish;
        dialogManager.sceneToLoadOnFinish = sceneToLoad;
        dialogManager.finishLoadDelay = loadDelay;


        // shows panel and adds sentences to dialog manager
        dialogManager.sentences = sentences;
        dialogManager.dialogKey = name;         
        if (dialogManager.DialogPanel != null)
            dialogManager.DialogPanel.SetActive(true);
        else
            dialogManager.gameObject.SetActive(true);

        
        triggered = true;
    }
}
