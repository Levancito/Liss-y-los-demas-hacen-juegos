using Builder;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Currency : MonoBehaviour, IResource
{
    public float _MoveSpeed { get; set; }
    public float _LifeSpan { get; set; }

    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private int Speed;
    private int LifeSpan = 30;


    private void Awake()
    {
        Speed = Random.Range(Speed, Speed + 2);
        resourceManager = FindObjectOfType<ResourceManager>();
        _MoveSpeed = Speed;
        _LifeSpan = LifeSpan * Speed;

        StartCoroutine(DestroyAfterLifeSpan());
    }

    public virtual void Move()
    {
        transform.position += Vector3.back * _MoveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        P_ShootController Player = collision.gameObject.GetComponent<P_ShootController>();

        if (Player != null)
        {
            if (resourceManager != null)
            {
                resourceManager.AddCurrency(1);
                Debug.Log("Se ha añadido 1 de Currency");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("ResourceManager no está asignado en ResourceStats.");
            }
        }
    }

    private IEnumerator DestroyAfterLifeSpan()
    {
        yield return new WaitForSeconds(LifeSpan);
        Debug.Log("Objeto destruido por alcanzar su tiempo de vida.");
        Destroy(gameObject);
    }
}


