using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFactory : Factory
{
    [SerializeField] private Bullet bulletPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        IProduct obj = Instantiate(bulletPrefab, position, transform.rotation);
        obj.Initialize();
        return obj;
    }
}