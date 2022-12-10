using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movemet")]
    public float _speed = 3f;
    [Header("Attack")]
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackspeed = 1f;
    private float _canAttack;

    [Header("Health")]
    private float _health;
    [SerializeField] private float _maxhealth;

    private Transform target;

    private void Start()
    {
        _health = _maxhealth;
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;
        Debug.Log("Enemy Health: " + _health);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_attackspeed <= _canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-_attackDamage);
                _canAttack = 0f;
            }
            else
            {
                _canAttack += Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            if (_attackspeed <= _canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-_attackDamage);
                _canAttack = 0f;
            }
            else
            {
                _canAttack += Time.deltaTime;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }
}
