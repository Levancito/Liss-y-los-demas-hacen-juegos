using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public int maxDamage = 90;
    public int minDamage = 10;
    public float maxDistance = 20f;
    public ParticleSystem shooting;
    private Transform player;
    public Animator animator;

    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            Fire();
            shooting.Play();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Fire()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        float t = Mathf.Clamp01(distance / maxDistance);
        int scaledDamage = Mathf.RoundToInt(Mathf.Lerp(maxDamage, minDamage, t)); 

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        animator.SetTrigger("Ship Shoot");

        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {

            enemyBullet.UpdateDamage(scaledDamage);
            enemyBullet.GetDirection();
        }
    }
}