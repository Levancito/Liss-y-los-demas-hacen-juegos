using Builder;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class Bullet : MonoBehaviour, IProduct
{
    public float speed = 10f;
    public int damage = 5;
    public float lifetime;
    public float currentlifetime;
    public RemoteConfig remoteconfig;

    private void Awake()
    {
        remoteconfig = GetComponent<RemoteConfig>();
        damage = RemoteConfigService.Instance.appConfig.GetInt("BulletDamage");
    }

    public void OnEnable()
    {
        currentlifetime = lifetime;
    }
    public void Initialize()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        currentlifetime -= Time.deltaTime;

        if(currentlifetime <= 0)
        {
            Release();

        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.HP -= damage;
            Release();
        }

    }

    void Release()
    {
        GetComponent<PooledObject>().Release();
    }

    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
    }
}
