using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;   // drag RespawnPoint here
    Rigidbody2D rb;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    public void Respawn()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        if (respawnPoint != null) transform.position = respawnPoint.position;
    }
}
