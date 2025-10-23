using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class MeleeEnemy : MonoBehaviour
{
    public GameObject firstPoint;
    public GameObject secondPoint;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    private GameObject player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = secondPoint.transform;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == secondPoint.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == secondPoint.transform)
        {
            flip();
            currentPoint = firstPoint.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == firstPoint.transform)
        {
            flip();
            currentPoint = secondPoint.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth currentHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (currentHealth != null)
            {
                currentHealth.TakeDamage();
            }
        }
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(firstPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(secondPoint.transform.position, 0.5f);
        Gizmos.DrawLine(firstPoint.transform.position, secondPoint.transform.position);
    }
}
