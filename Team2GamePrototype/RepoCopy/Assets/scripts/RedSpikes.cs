using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedSpikes : MonoBehaviour
{
    [Header("Optional UI message")]
    public TMP_Text output;                  // drag your TMP text here if you want a message
    [TextArea] public string message = "You died. Try again";

    void Reset()
    {
        // ensure this collider is set as a trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // show message (optional)
        if (output) { output.text = message; output.enabled = true; }

        // respawn the player
        var respawn = other.GetComponent<PlayerRespawn>();
        if (respawn != null) respawn.Respawn();
    }
}
