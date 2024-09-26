using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IProduct
{
    private float speed = 20f;
    private int damage = 10;
    private Rigidbody rb;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * -1f * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (other.CompareTag("Player"))
            {
                damageable.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void GetDirection()
    {
        Transform target = FindObjectOfType<Stats>().transform;
    }
}
