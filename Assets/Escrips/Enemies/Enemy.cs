using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Renderer))]
public class Enemy : MonoBehaviour, IDamageable, IMovable
{
    public int MaxHP { get; set; }
    public float Speed { get; set; }
    public float Escala { get; set; }
    public float Position { get; set; }
    public float Rotation { get; set; }
    public float Dmg { get; set; }
    public int HP { get; set; }

    public Enemy(int health, float speed, float scale)
    {
        MaxHP = health;
        Speed = speed;
        Escala = scale;
        HP = health; 
    }

    protected virtual void Awake()
    {
        HP = MaxHP; 
    }

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject); 
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        MaxHP = newMaxHealth;
    }

    public virtual void Heal(int amount)
    {
        HP += amount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }
}

