using Builder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProduct
{
    public float speed = 10f;
    public float damage = 5;

    public void Initialize()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, Vector3.zero) > 100f)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.MaxHealth -= damage; 
            Destroy(gameObject);
        }
    }

   
}
