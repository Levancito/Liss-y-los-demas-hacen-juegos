using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuerca : MonoBehaviour, IResource
{
    public float _MoveSpeed { get; set; }
    public float _LifeSpan { get; set; }

    [SerializeField] private ResourceManager resourceManager;

    [SerializeField] private int Speed;

    [SerializeField] private int Healing;

    private int LifeSpan = 30;


    private void Awake()
    {

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
        if (resourceManager != null) resourceManager = FindObjectOfType<ResourceManager>();

        if (Player != null)
        {
            Stats PlayerStats = Player.GetComponent<Stats>();

            if (PlayerStats.MaxHP == PlayerStats.HP)
            {
                if (resourceManager != null)
                {
                    resourceManager.AddTuerca(1);
                    Debug.Log("Se ha añadido 1 de Currency");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("ResourceManager no está asignado en ResourceStats.");
                }
            }
            else
            {
                PlayerStats.Heal(Healing);
                Destroy(gameObject);

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
