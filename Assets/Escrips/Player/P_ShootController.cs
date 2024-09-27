using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.UIElements;

public class P_ShootController : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    public P_JoystickController joystickController;
    public float shootInterval = 0.1f;
    private float shootTimer = 0f;
    public Transform shootingpos;

    private void Update()
    {

        Vector2 moveDir = joystickController.MoveDir;

        if (moveDir.magnitude > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot(shootingpos.position);
                shootTimer = shootInterval;
            }
        }
    }

    public void UpdateShootInterval(float newInterval)
    {
        shootInterval = newInterval;
    }

    private void Shoot(Vector3 position)
    {
        _pool.GetPooledObject(position, shootingpos.rotation);
    }
}