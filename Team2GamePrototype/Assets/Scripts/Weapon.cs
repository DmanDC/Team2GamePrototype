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
    private Animator animator;

    void Start()
    {
        // Try to find Animator on this object or its parent
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }


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

        if (animator != null)
            animator.SetBool("isShooting", true);

        for (int i = 0; i < burstFireNumShots; i++)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(timeBtwnShots);
        }

        if (animator != null)
            animator.SetBool("isShooting", false);

        yield return new WaitForSeconds(timeBtwnShots);
        canFire = true;
    }
}

