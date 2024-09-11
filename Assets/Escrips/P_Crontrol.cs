using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Crontrol : MonoBehaviour
{
    [SerializeField] P_Controller _controller;
    [SerializeField] float _speed;

    void Update()
    {
        transform.position += _controller.GetMovementInput() * _speed * Time.deltaTime;
    }

}
