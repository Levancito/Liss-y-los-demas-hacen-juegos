using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_ShootController : MonoBehaviour
{
    public P_JoystickController joystickController;
    public bulletFactory bulletFactory;
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float shootInterval = 0.1f;  
    private float shootTimer = 0f;  

    private void Update()
    {
        if (joystickController == null)
        {
            Debug.LogError("JoystickController no est� asignado.");
            return;
        }

        Vector2 moveDir = joystickController.MoveDir;

        if (moveDir.magnitude > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Vector3 shootDirection = new Vector3(moveDir.x, 0, moveDir.y);
                Shoot(shootDirection);
                shootTimer = shootInterval;
            }
        }

    }

    private void Shoot(Vector3 direction)
    {
        bulletFactory.GetProduct(direction);
    }
}
