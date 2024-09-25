using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

namespace Builder
{
    [RequireComponent(typeof(Renderer))]
    public class Enemy : MonoBehaviour
        //a esto se le podria agregar 2 interfaces: IDamageable e IStats, una con los metodos de daño curacion y muerte y otra con los stats del enemigo
        //como se generan con un spawner tambien se le puede asignar una IProduct que tenga el metodo que se ejecuta al instanciarlo
    {
        public float MaxHealth;// estos dos pueden quedar declarados en IStats
        public float Speed;

        public Color Color
        {
            get => renderer.material.color; 
            set => renderer.material.color = value;
        }
        private new Renderer renderer;


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (MaxHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public class EnemyBuilder //las cosas del builder deberian estar en una clase a parte que se encarge de buildear enemigos
    {
        private Enemy prefab;
        private float MaxHealth;
        private float Speed;
        private Vector3 position;
        private Quaternion rotation;
        private Bullet bullet;
        private Vector3 scale;
        
        public EnemyBuilder(Enemy prefab)
        {
            this.prefab = prefab;
            MaxHealth = 16;
            Speed = 5;
            position = Vector3.zero;
            rotation = Quaternion.identity;
            scale = Vector3.one;
        }

        public Enemy Build()
        {
            Enemy instance = Object.Instantiate(prefab, position, rotation);
            instance.transform.localScale = scale;
            instance.MaxHealth = MaxHealth;
            instance.Speed = Speed;
            return instance;
        }

        //a partir de aca no hace falta que sean funciones separadas, se puede definir el objeto de tipo enemy que pida todo esto y darselo en cada instancia
        public EnemyBuilder SetMaxHealth(float maxHealth)
        {
            this.MaxHealth = maxHealth;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed)
        {
            this.Speed = speed;
            return this;
        }

        public EnemyBuilder SetPosition(Vector3 position)
        {
            this.position = position;
            return this;
        }

        public EnemyBuilder SetPosition(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
            return this;
        }
        public EnemyBuilder SetRoatation(Quaternion rotation)
        {
            this.rotation = rotation;
            return this;
        }

        public EnemyBuilder SetRoatation(Vector3 euler)
        {
            rotation = Quaternion.Euler(euler);
            return this;
        }

        public EnemyBuilder SetScale(Vector3 scale) 
        {
            this.scale = scale;
            return this;
        }

        public EnemyBuilder SetScale(float scale)
        {
            this.scale = new Vector3(scale, scale, scale);
            return this;
        }

        //------------------------------------------------------------------
        //ej:
        //-----Interfaces-----
        // IStats -  trae HP, velocidad, escala etc.
        // IDamageable:IStats - metodo para tomar daño y para morir
        // IMovable:Istats - metodos para mover al bichito
        // Enemy:Monobehaviour, IDamageable - Implementa todo lo de las interfaces. Agrega definicion de Enemy:
        //
        // public abstract class Enemy:Monobehaviour, IDamageable, IMovable
        //{
        //  Implementa Interfaces
        //
        //  public Enemy(int Health, Speed, Scale)
        //  {
        //      HP = Health
        //      velocidad = Speed
        //      escala = Scale
        //  }
        //
        //  virtual void TakeDamage(int damage)
        //  {
        //      logica de daño
        //  }
        //  abstract void TodoLoQueNoSeUseAca(); --> Die etc si se quieren hacer especificos para cada enemigo
        //}
        //
        //
        //Subclase que hereda de enemigo:
        //
        //public class Ship:Enemy
        //{
        //  public Ship():base(Health, Speed, Scale) { } ----> en Health Speed Scale van los valores que se quieran asignar, se define dentro de la clase especifica
        //
        //  metodos especificos de esta clase (tipo die ponele, o si tiene algun comportamiento particular de esta clase)
        //}
    }
}