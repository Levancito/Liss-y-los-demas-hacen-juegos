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
        if (remoteconfig != null)
        {
            speed = RemoteConfigService.Instance.appConfig.GetFloat("BulletSpeed", speed);
            damage = RemoteConfigService.Instance.appConfig.GetInt("BulletDamage", damage);
        }
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
    public void UpdateDamage(int newDamage, float newSpeed)
    {
        damage = newDamage;
        speed = newSpeed;
    }
    public void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            Debug.Log($"Disparo impactó al enemigo y le hace {damage} de daño.");
            enemy.TakeDamage(damage); 
            Release();
        }
    }

    void Release()
    {
        GetComponent<PooledObject>().Release();
    }

    
}
