using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFactory : Factory
{
    [SerializeField] private Bullet bulletPrefab;
    public Transform shootingpos;
    public override IProduct GetProduct(Vector3 position)
    {
        IProduct obj = Instantiate(bulletPrefab, shootingpos.position, Quaternion.Euler(0,-90, 0));
        obj.Initialize();
        return obj;
    }
}