using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int MaxHP { get; set; }
    int HP {  get; set; }

    void TakeDamage(int damage);
    void Heal(int health);
    void Die();
}
