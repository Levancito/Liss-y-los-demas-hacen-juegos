using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
{
    float MoveSpeed { get; set; }

    void Move();
}
