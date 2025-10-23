using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class TriggerRock : MonoBehaviour
{
    [Header("Loop Settings")]
    public Transform spawnPoint;          // leave empty to use the rock's start position
    public float resetDelay = 0.35f;      // wait before rock resets after hitting ground
    public float extraLift = 0.0f;        // add a tiny height when respawning (optional)

    [Header("Ground Detection")]
    public LayerMask groundLayers;        // set to your Ground layer in Inspector

    [Header("Optional: On player hit")]
    public UnityEvent onHitPlayer;        // e.g., show text, play sound, etc.

    Rigidbody2D rb;
    Vector3 startPos;
    bool resetting;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = (spawnPoint != null) ? (Vector3)spawnPoint.position : transform.position;

        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If we hit the player -> fire event and respawn the player
        if (collision.collider.CompareTag("Player"))
        {
            onHitPlayer?.Invoke();

            var respawn = collision.collider.GetComponent<PlayerRespawn>();
            if (respawn != null) respawn.Respawn();

            // Optionally also reset the rock
            if (!resetting) StartCoroutine(ResetAfterDelay());
            return;
        }

        // If we hit the ground layer -> schedule a reset
        if (IsInLayerMask(collision.collider.gameObject.layer, groundLayers))
        {
            if (!resetting) StartCoroutine(ResetAfterDelay());
        }
    }

    System.Collections.IEnumerator ResetAfterDelay()
    {
        resetting = true;
        yield return new WaitForSeconds(resetDelay);

        // stop motion & teleport back to spawn
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Vector3 target = startPos + Vector3.up * extraLift;
        transform.position = target;

        // clean physics contacts and fall again
        rb.Sleep();
        rb.WakeUp();

        resetting = false;
    }

    bool IsInLayerMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}
