using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Currency : MonoBehaviour, IResource
{
    public float MoveSpeed { get; set; }

    [SerializeField] private ResourceManager resourceManager;

    [SerializeField] private int Speed;

    private void Awake()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        MoveSpeed = Speed;
    }
    public virtual void Move()
    {
        transform.position += Vector3.back * MoveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        P_Controller pControl = collision.gameObject.GetComponent<P_Controller>();
        if (pControl != null) 
        {
            if (resourceManager != null)
            {
                resourceManager.AddCurrency(1);
                Debug.Log("Se ha a�adido 1 de Currency");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("ResourceManager no est� asignado en ResourceStats.");
            }
        }
    }
}

