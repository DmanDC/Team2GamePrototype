using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform shotPoint;
    public Transform gun;
    public GameObject enemyBullet;

    public float followPlayerRange = 10f;
    private bool inRange;
    public float attackRange = 6f;

    public float startTimebtwnShots = 1.5f;
    private float timeBtwnShots;

    private Transform player;   // ← resolve at runtime

    void Start()
    {
        TryResolvePlayer();
        timeBtwnShots = 0f;
    }

    void Update()
    {
        if (player == null) TryResolvePlayer();
        if (player == null) return;

        // Aim gun
        Vector3 difference = player.position - gun.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        float dist = Vector2.Distance(transform.position, player.position);
        inRange = (dist < followPlayerRange && dist > attackRange);

        // --- cooldown tick (was missing)
        if (timeBtwnShots > 0f) timeBtwnShots -= Time.deltaTime;

        if (dist <= attackRange)
        {
            if (timeBtwnShots <= 0f)
            {
                Instantiate(enemyBullet, shotPoint.position, shotPoint.transform.rotation);
                timeBtwnShots = startTimebtwnShots;
            }
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;
        if (inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void TryResolvePlayer()
    {
        var go = GameObject.FindGameObjectWithTag("Player");
        player = go != null ? go.transform : null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followPlayerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
