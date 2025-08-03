using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Enemy : MonoBehaviour, IDamageable, IMovable
{
    public int MaxHP { get; set; }
    public float Speed { get; set; }
    public float Escala { get; set; }
    public float Position { get; set; }
    public float Rotation { get; set; }
    public float Dmg { get; set; }
    [SerializeField] public int HP { get; set; }

    //private WinCondition winCondition;


    public Enemy(int health, float speed, float scale)
    {
        MaxHP = health;
        Speed = speed;
        Escala = scale;
        HP = health;
    }


    protected virtual void Awake()
    {

        UpdateMaxHealth(MaxHP);
        if (HP == 0)
        {
            HP = MaxHP;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        MaxHP = newMaxHealth;
        HP = MaxHP;
    }
    public void UpdateSpeed(float EnemySpeed)
    {
        Speed = EnemySpeed;
    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log("tomo da�o" + damage);
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //WinCondition winCondition = FindAnyObjectByType<WinCondition>();
        //winCondition.WinAdd();
        EventManager.TriggerEvent(EventsType.Event_EnemyDestroyed, this);
        
        ResourceSpawner resourceSpawner = FindObjectOfType<ResourceSpawner>();
        if (resourceSpawner != null) 
        {
            var numero = Random.Range(0,101);
            if (numero <= 20)
            {
                Debug.Log(numero);
                resourceSpawner.SpawnTuerca(gameObject.transform);
            }
        }
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }
    public virtual void Heal(int amount)
    {
        HP += amount;
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }
}

