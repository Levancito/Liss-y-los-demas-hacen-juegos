using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 20f;
    public int damage = 10;
    private Rigidbody rb;

    public void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward *  speed;
        
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
        }
    }

    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void GetDirection()
    {
        Transform target = FindObjectOfType<Stats>().transform;
    }
}