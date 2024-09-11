
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class P_JoystickController : P_Controller, IDragHandler, IEndDragHandler
{
    public Vector3 _initialPosition;
    [SerializeField] float _maxmagnitude = 125;
    void Start()
    {
        _initialPosition = transform.position;
    }
    public override Vector3 GetMovementInput()
    {
        Vector3 modifiedDir = new Vector3(_moveDir.x,0, _moveDir.y);
        return modifiedDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - _initialPosition, _maxmagnitude);
        transform.position = _initialPosition + _moveDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initialPosition;
        _moveDir = Vector3.zero;
    }

}
