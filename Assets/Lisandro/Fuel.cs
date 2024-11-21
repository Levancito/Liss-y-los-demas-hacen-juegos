using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Fuel : MonoBehaviour, IResource
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
                resourceManager.AddStamina(1);
                Debug.Log("Se ha añadido 1 de Currency");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("ResourceManager no está asignado en ResourceStats.");
            }
        }
    }
}
