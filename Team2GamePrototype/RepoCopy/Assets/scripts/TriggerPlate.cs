using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerPlate : MonoBehaviour
{
    [Header("Optional UI")]
    public TMP_Text output;                       // drag a TMP_Text if you want a message
    [TextArea] public string message = "You died. Try again";

    void Reset()
    {
        // make sure the collider acts as a trigger
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
