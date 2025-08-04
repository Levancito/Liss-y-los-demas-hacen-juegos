using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private Pool pool;

    public Pool Pool
    {
        
        get => pool;

        set => pool = value;
    }


    public void Release()
    {
        pool.ReturnToPool(pooledObject: this);
    }
}
