using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbProjectile : MonoBehaviour
{
    [HideInInspector] public float OrbVelocity;
    [HideInInspector] public float OrbDamage;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * OrbVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Attacked");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(OrbDamage);
        }
        Destroy(gameObject);
    }
}

