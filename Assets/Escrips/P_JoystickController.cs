
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class P_JoystickController : P_Controller, IDragHandler, IEndDragHandler
{
    public Vector3 _initialPosition;
    [SerializeField] float _maxmagnitude = 125;

    public Vector2 MoveDir { get; private set; }

    void Start()
    {
        _initialPosition = transform.position;
    }

    public override Vector3 GetMovementInput()
    {
        Vector3 modifiedDir = new Vector3(MoveDir.x, 0, MoveDir.y);
        return modifiedDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - _initialPosition, _maxmagnitude);
        transform.position = _initialPosition + moveDir;
        MoveDir = new Vector2(moveDir.x, moveDir.y); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initialPosition;
        MoveDir = Vector2.zero;
    }
}
