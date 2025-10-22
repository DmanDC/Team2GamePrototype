using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int burstFireNumShots = 3;
    public float timeBtwnShots = 0.1f;
    public float burstCoolDown = 0.5f;

    private bool canFire = true;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        canFire = false;
        for (int i = 0; i < burstFireNumShots; i++)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(timeBtwnShots);
        }
        yield return new WaitForSeconds(timeBtwnShots);
        canFire = true;
    }
}

