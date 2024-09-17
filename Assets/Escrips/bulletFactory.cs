using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFactory : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform bulletSpawnPoint;  
    public float bulletSpeed = 10f; 

    public GameObject CreateBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Bullet bullet_S = bullet.GetComponent<Bullet>();
        if (bullet_S != null)
        {
            bullet_S.speed = bulletSpeed;
        }

        return bullet;
    }
}