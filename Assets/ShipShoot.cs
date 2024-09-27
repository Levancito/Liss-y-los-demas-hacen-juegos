using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto de donde se disparará la bala
    public float fireRate = 0.5f; // Frecuencia de disparo

    private void Start()
    {
        Debug.Log("Enemigo iniciado, comenzando a disparar.");
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Fire()
    {
        Debug.Log("Disparando...");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            enemyBullet.GetDirection();
        }
    }
}