using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Enemy
{
    private GameObject player;

    [SerializeField]
    private float movementSpeed = 4f;
    [SerializeField]
    private float Amplitud = 5f; 
    [SerializeField]
    private float descentSpeed;

    private Vector3 initialPosition; 
    private float angle; 

    public Ship(int health, float speed, float scale) : base(health, speed, scale)
    {
        MaxHP = health;
        Speed = speed;
        Escala = scale;
        HP = health;
    }

    private void Start()
    {
        movementSpeed = Speed;
        initialPosition = transform.position; 
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); 
        }
    }

    private void FixedUpdate()
    {
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

        angle += movementSpeed * Time.deltaTime;

        float offsetX = Mathf.Sin(angle) * Amplitud;

        float newZ = transform.position.z - descentSpeed * Time.deltaTime;

        Vector3 targetPosition = (initialPosition + player.transform.position) / 2;

        transform.position = new Vector3(
            targetPosition.x + offsetX,
            transform.position.y,
            newZ
        );
    }
}




