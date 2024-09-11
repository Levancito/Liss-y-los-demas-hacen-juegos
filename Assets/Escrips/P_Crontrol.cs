using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Crontrol : MonoBehaviour
{
    [SerializeField] P_Controller _controller;
    [SerializeField] float _speed;
    [SerializeField] float _max = 0.1f;

    private Camera _camera;
    private float _screenWidth;
    private float _screenHeight;

    void Start()
    {
        _camera = Camera.main;
        _screenWidth = _camera.orthographicSize * _camera.aspect;
        _screenHeight = _camera.orthographicSize;
    }

    void Update()
    {
        Vector3 moveInput = _controller.GetMovementInput() * _speed * Time.deltaTime;
        Vector3 newPosition = transform.position + moveInput;


        float xMin = -_screenWidth + _max;
        float xMax = _screenWidth - _max;
        float yMin = -_screenHeight + _max;
        float yMax = _screenHeight - _max;

        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);

        transform.position = newPosition;
    }

}
