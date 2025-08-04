using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private Pool pool;
    public float moveSpeed = 3f; 

    private void Start()
    {
        transform.position = new Vector3(0, 0, 52);
    }

    private void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        P_ShootController Player = collision.gameObject.GetComponent<P_ShootController>();

        if (Player != null) 
        {
            pool.PowerUp = true;

            Destroy(gameObject);
        }
        //if (collision.gameObject.CompareTag("Player"))
        //{

        //    pool.PowerUp = true;

        //    Destroy(gameObject);
        //}
    }

}
