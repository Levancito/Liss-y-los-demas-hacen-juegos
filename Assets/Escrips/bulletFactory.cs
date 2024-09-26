using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFactory : Factory
{
    [SerializeField] private Bullet bulletPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        IProduct obj = Instantiate(bulletPrefab, position, Quaternion.identity);
        obj.Initialize();
        return obj;
    }
}