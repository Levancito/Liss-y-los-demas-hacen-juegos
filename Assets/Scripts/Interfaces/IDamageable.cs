using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int MaxHP { get; set; }
    public int HP { get; set; }

    void TakeDamage(int damage);
    void Heal(int health);
    void Die();
}
