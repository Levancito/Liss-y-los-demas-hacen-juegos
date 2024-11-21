using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
{
    float _MoveSpeed { get; set; }
    float _LifeSpan { get; set; }
    void Move();
}
