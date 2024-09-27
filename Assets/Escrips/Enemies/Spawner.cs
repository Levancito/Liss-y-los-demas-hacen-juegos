using UnityEngine;

namespace Builder
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        public Ship shipPrefab;
        [SerializeField]
        public Plane planePrefab;

        [SerializeField]
        private int EnemyQuantity;
        [SerializeField]
        private int Level;

        [SerializeField]
        private GameObject player;

        private void Awake()
        {
            if (player == null)
            {
                return;
            }

            for (int i = 0; i < EnemyQuantity; i++)
            {
                Vector3 randomPosition = new Vector3(player.transform.position.x + Random.Range(-20f, 20f), 0, Random.Range(0, 50));

                Ship ship = new EnemyBuilder<Ship>(shipPrefab)
                    .Build(
                        MaxHP: 5 + Level,              // Salud máxima
                        speed: Random.Range(1,4),                  // Velocidad
                        position: randomPosition,                 // Posición aleatoria
                        rotation: Quaternion.identity,            // Rotación por defecto
                        scale: Vector3.one * (1 + Level / 2f)   // Escala basada en el nivel
                        );

            }
            for (int i = 0; i < EnemyQuantity * 2; i++)
            {
                Vector3 randomPosition = new Vector3(player.transform.position.x + Random.Range(-30f, 30f), 0, Random.Range(0, 50));

                Plane plane = new EnemyBuilder<Plane>(planePrefab)
                    .Build(
                        MaxHP: 15 + Level,              // Salud máxima
                        speed: 4 + Level / 3f,                  // Velocidad
                        position: randomPosition,                 // Posición aleatoria
                        rotation: Quaternion.identity,            // Rotación por defecto
                        scale: Vector3.one * (1 + Level / 3f)   // Escala basada en el nivel
                        );
            }
        }
    }
}



