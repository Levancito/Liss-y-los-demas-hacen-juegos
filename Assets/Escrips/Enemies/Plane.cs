using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Enemy
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float movementSpeed; 
    [SerializeField]

    public Plane(int health, float speed, float scale) : base(health, speed, scale)
    {
        MaxHP = health;
        Speed = speed;
        Escala = scale;
        HP = health; 
    }
    private void Start()
    {
        this.movementSpeed = Speed;
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); // Buscar al jugador por tag
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("EL AVION TIENE " + HP);
        Move();
    }

    public override void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("La nave ha sido destruida.");
        Destroy(gameObject);
    }
}
