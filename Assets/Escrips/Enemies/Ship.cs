using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Enemy
{

    [SerializeField]
    private GameObject player; 
    [SerializeField]
    private float movementSpeed; 
    [SerializeField]
    private float oscillationAmplitude = 1f;
    [SerializeField]
    private float oscillationFrequency = 2f;

    public Ship(int health, float speed, float scale) : base(health, speed, scale) 
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
    protected override void Awake()
    {
        base.Awake();
        if (HP == 0)
        {
            HP = MaxHP;
        }
    }
    private void FixedUpdate()
    {
        Debug.Log(HP);
        Move();
    }

    public override void Die()
    {
        Debug.Log("La nave ha sido destruida.");
        Destroy(gameObject);
    }

    public override void Move()
    {
        if (player == null) return;

        // Calcular la oscilación en el eje X
        float oscillation = Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude;

        // Calcular la posición deseada en el eje X (donde está el jugador)
        float desiredXPosition = player.transform.position.x + oscillation;

        // Mover la nave hacia la posición deseada
        Vector3 targetPosition = new Vector3(desiredXPosition, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }
}