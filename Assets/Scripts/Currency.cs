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

    public AudioSource Audio;
    public AudioClip AudioClip;

    private void Awake()
    {
        Speed = Random.Range(Speed, Speed + 2);
        resourceManager = FindObjectOfType<ResourceManager>();

        _MoveSpeed = Speed;
        _LifeSpan = LifeSpan * Speed;

        StartCoroutine(DestroyAfterLifeSpan());
    }

    private void Start()
    {
        
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
                resourceManager.AddCurrency(1);
                Destroy(gameObject);
            }
            else
            {
            }
        }
    }

    private IEnumerator DestroyAfterLifeSpan()
    {
        yield return new WaitForSeconds(LifeSpan);
        Destroy(gameObject);
    }
}


