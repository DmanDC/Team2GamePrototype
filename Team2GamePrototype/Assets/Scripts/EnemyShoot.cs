using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Re-acquire if lost/not yet spawned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return; // still nothing this frame
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 5f)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

}
