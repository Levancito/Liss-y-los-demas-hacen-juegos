using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class Fuel : MonoBehaviour, IResource
{

    public float _MoveSpeed { get; set; }
    public float _LifeSpan { get; set; }

    [SerializeField] private ResourceManager resourceManager;

    [SerializeField] private int Speed;

    private int LifeSpan = 30;

    public AudioSource Audio;
    public AudioClip AudioClip;
    private void Awake()
    {
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
        Audio.PlayOneShot(AudioClip);
        if (Player != null)
        {
            if (resourceManager != null)
            {
                resourceManager.AddStamina(1);
                Debug.Log("Se ha a�adido 1 de Currency");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("ResourceManager no est� asignado en ResourceStats.");
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
