using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.UI;

public class P_Crontrol : MonoBehaviour
{
    public enum ControlType { Joystick, Gyro }

    [Header("Tipo de control")]
    [SerializeField] public ControlType tipoDeControl;

    [Header("Referencias de controladores")]
    [SerializeField] private P_JoystickController joystickController;
    [SerializeField] private P_GyroController gyroController;
    [SerializeField] private P_Animations animaciones;

    [Header("Movimiento")]
    [SerializeField] public float _speed = 0.4f;
    [SerializeField] public float _max = 0.1f;

    private P_Controller _controller;
    private Camera _camera;
    private float _screenWidth;
    private float _screenHeight;

    void Awake()
    {
        tipoDeControl = (ControlType)PlayerPrefs.GetInt("ControlType",0); 
        _speed = RemoteConfigService.Instance.appConfig.GetFloat("PlayerSpeed");

        switch (tipoDeControl)
        {
            case ControlType.Joystick:
                _controller = joystickController;
                break;
            case ControlType.Gyro:
                _controller = gyroController;
                break;
        }
    }

    void Start()
    {
        _camera = Camera.main;
        _screenWidth = _camera.orthographicSize * _camera.aspect;
        _screenHeight = _camera.orthographicSize;
    }

    void Update()
    {
        if (_controller == null) return;

        Vector3 moveInput = _controller.GetMovementInput() * _speed * Time.deltaTime;
        Vector3 newPosition = transform.position + moveInput;
        animaciones.ReportarMovimiento(moveInput);

        float xMin = -_screenWidth + _max;
        float xMax = _screenWidth - _max;
        float zMin = -_screenHeight + _max;
        float zMax = _screenHeight - _max;

        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);

        transform.position = newPosition;
    }

    public void UpdateSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void SetControlType(ControlType nuevoTipo)
    {
        tipoDeControl = nuevoTipo;

        switch (tipoDeControl)
        {
            case ControlType.Joystick:
                _controller = joystickController;
                break;
            case ControlType.Gyro:
                _controller = gyroController;
                break;
        }
    }
}