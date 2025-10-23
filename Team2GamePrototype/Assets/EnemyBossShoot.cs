using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemyBossShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;
    public int burstFireNumShots = 3;
    public float timeBtwnShots = 0.01f;
    public float burstCoolDown = 0.2f;

    bool canFire = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return; 
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 10f)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    IEnumerator Shoot()
    {
        canFire = false;


        for (int i = 0; i < burstFireNumShots; i++)
        {
            Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            yield return new WaitForSeconds(timeBtwnShots);
        }

        yield return new WaitForSeconds(timeBtwnShots);
        canFire = true;
    }

}
