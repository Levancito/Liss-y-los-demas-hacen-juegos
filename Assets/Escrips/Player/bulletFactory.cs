using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bulletFactory : Factory
{
    [SerializeField] private Bullet bulletPrefab;
    public override IProduct GetProduct(Vector3 position)
    {
        IProduct obj = Instantiate(bulletPrefab, position, Quaternion.Euler(0, -90, 0));
        obj.Initialize();
        return obj;
    }
}
