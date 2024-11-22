using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour, IDamageable
{
    public int MaxHP { get; set; }
    public int HP { get; set; }

    public Image HealthBar;
    void Awake()
    {
        MaxHP = 100;
        HP = MaxHP;
    }
    public void Start()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        UpdateUI();
        if (HP <= 0)
        {
            Die();
        }
        Debug.Log(HP);
    }
    public virtual void Heal(int health)
    {
        HP += health;
        UpdateUI();
        Mathf.Clamp(HP, 0, MaxHP);
    }
    public virtual void Die()
    {
        EventManager.TriggerEvent(EventsType.Event_Defeat, this);
        //Destroy(gameObject);
        //cualquier otra logica de GameOver tipo pantalla etc
        //probablemente es mejor deshabilitar todo lo que sea player en vez de destruirlo de una
    }

    void UpdateUI()
    {

        if (HealthBar != null)
        {
            HealthBar.fillAmount = (float)HP / MaxHP;
        }
    }
}
