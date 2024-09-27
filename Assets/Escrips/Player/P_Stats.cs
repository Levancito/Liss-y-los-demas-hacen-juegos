using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    public int MaxHP {  get; set; }
    public int HP {  get; set; }

    public void Start()
    {
        MaxHP = 100;
        HP = MaxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
        Debug.Log(HP);
    }
    public virtual void Heal(int health)
    {
        HP += health;
        Mathf.Clamp(HP, 0, MaxHP);
    }
    public virtual void Die()
    {
        Destroy(gameObject);
        //cualquier otra logica de GameOver tipo pantalla etc
        //probablemente es mejor deshabilitar todo lo que sea player en vez de destruirlo de una
        Time.timeScale = 0;
    }
}
