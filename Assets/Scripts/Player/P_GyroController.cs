using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GyroController : P_Controller
{
    [SerializeField] private float sensitivity = 80f;

    private void Start()
    {
        if (!SystemInfo.supportsAccelerometer)
        {
            Debug.LogWarning("no soporta acelerómetro.");
        }
    }

    public override Vector3 GetMovementInput()
    {
        if (!SystemInfo.supportsAccelerometer)
            return Vector3.zero;

        Vector3 acceleration = Input.acceleration;

        float x = acceleration.x;
        float z = acceleration.y;

        Vector3 move = new Vector3(x, 0f, z);

        if (move.magnitude < 0.1f)
            return Vector3.zero;

        return move.normalized * sensitivity;
    }
}

