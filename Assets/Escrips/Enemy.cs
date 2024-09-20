using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

namespace Builder
{
    [RequireComponent(typeof(Renderer))]
    public class Enemy : MonoBehaviour
    {
        public float MaxHealth;
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

 
    }

    public class EnemyBuilder
    {
        private Enemy prefab;
        private float MaxHealth;
        private float Speed;
        private Vector2 position;
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
    }
}