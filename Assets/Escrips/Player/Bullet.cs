using Builder;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class Bullet : MonoBehaviour, IProduct
{
    public float speed = 10f;
    public int damage = 5;
    public RemoteConfig remoteconfig;

    private void Awake()
    {
        remoteconfig = GetComponent<RemoteConfig>();
        damage = RemoteConfigService.Instance.appConfig.GetInt("BulletDamage");
    }

    public void Initialize()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, Vector3.zero) > 100f)
        {
            ReturnToPool();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.MaxHP -= damage;
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        Pool pool = FindObjectOfType<Pool>();
        if (pool != null)
        {
            pool.ReturnBullet(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
    }
}
