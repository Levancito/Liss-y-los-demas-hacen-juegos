using UnityEngine;

namespace Builder
{
    public class EnemyBuilder<T> where T : Enemy
    {
        private T prefab;
        private int MaxHP;
        private float Speed;
        private int HP;
        private Vector3 position;
        private Quaternion rotation;
        private Vector3 scale;

        public EnemyBuilder(T prefab)
        {
            this.prefab = prefab;
            MaxHP = 15;
            Speed = 5;
            position = Vector3.zero;
            rotation = Quaternion.identity;
            scale = Vector3.one;
        }

        // Método unificado para construir un enemigo con todos los parámetros
        public T Build(int MaxHP = 15, float speed = 5, Vector3? position = null, Quaternion? rotation = null, Vector3? scale = null)
        {
            this.MaxHP = MaxHP;
            this.Speed = speed;
            this.position = position ?? Vector3.zero;
            this.rotation = rotation ?? Quaternion.identity;
            this.scale = scale ?? Vector3.one;

            T instance = Object.Instantiate(prefab, this.position, this.rotation);
            instance.transform.localScale = this.scale;
            instance.MaxHP = this.MaxHP;
            instance.Speed = this.Speed;
            instance.HP = this.MaxHP;
            return instance;
        }
    }
}