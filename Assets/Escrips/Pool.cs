using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int poolSize = 3;

    private List<Bullet> bullets;

    private void Awake()
    {
        bullets = new List<Bullet>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
        }
        
    }

    public Bullet GetBullet(Vector3 position)
    {
        foreach (var bullet in bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.gameObject.SetActive(true);
                bullet.Initialize(); 
                return bullet;
            }
        }
        Debug.Log("FullPool");
        return null;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); 
    }
}
