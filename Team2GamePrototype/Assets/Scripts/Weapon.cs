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
    public float burstCooldown = 0.5f;

    private bool canFire = true;

    // NEW: animator reference
    private Animator anim;

    void Awake()
    {
        // If Weapon is on the Player object, this finds the Animator.
        // If Weapon is on a child object, GetComponent<Animator>() will be null,
        // so fall back to the parent's Animator.
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        // Single-burst on click (same as your code)
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            StartCoroutine(Shoot());
        }

        // OPTIONAL: If you want hold-to-fire bursts, use GetButton instead:
        // if (Input.GetButton("Fire1") && canFire) { StartCoroutine(Shoot()); }
    }
<<<<<<< HEAD

=======
>>>>>>> ae9a9fe72ab3c86d01775e541a9b9517b896788f
    IEnumerator Shoot()
    {
        canFire = false;

        // Start shooting animation
        if (anim != null) anim.SetBool("IsShooting", true);

        for (int i = 0; i < burstFireNumShots; i++)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(timeBtwnShots);
        }

        // Stop shooting animation when the burst ends
        if (anim != null) anim.SetBool("IsShooting", false);

        // Respect cooldown before next burst
        yield return new WaitForSeconds(burstCooldown);

        canFire = true;
    }
}

