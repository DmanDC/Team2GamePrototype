using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed = 3f;
    public float distanceBetween = 8f;

    private Transform target;    // ← use Transform, resolve at runtime
    private float distance;

    void Start()
    {
        TryResolvePlayer();
    }

    void Update()
    {
        if (target == null) TryResolvePlayer();
        if (target == null) return; // still no player, bail safely

        distance = Vector2.Distance(transform.position, target.position);
        Vector2 direction = target.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void TryResolvePlayer()
    {
        var go = GameObject.FindGameObjectWithTag("Player");
        target = go != null ? go.transform : null;
    }
}
