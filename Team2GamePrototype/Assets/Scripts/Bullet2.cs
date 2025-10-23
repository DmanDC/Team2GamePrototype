using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet2 : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage = 25f;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy2 enemy = hitInfo.GetComponentInParent<Enemy2>();
        if (enemy != null)
        {
            Debug.Log("Hit" + hitInfo.gameObject.name);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

       

        // if bullet hits wall, destroy bullet
        if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Bullet has impacted the platform!");
            Destroy(gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
