using UnityEngine;

namespace Builder
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Ship shipPrefab;
        [SerializeField]
        private Plane planePrefab;

        [SerializeField]
        private int enemyQuantity;
        [SerializeField]
        private int level;

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private float spawnInterval = 5f; 

        private float nextSpawnTime;

        private void FixedUpdate()
        {
            if (Time.time >= nextSpawnTime)
            {
                Spawn();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        private void Spawn()
        {
            if (player == null)
            {
                return;
            }

            SpawnShips();
            SpawnPlanes();
        }

        private void SpawnShips()
        {
            for (int i = 0; i < enemyQuantity; i++)
            {
                Vector3 randomPosition = new Vector3(
                    player.transform.position.x + Random.Range(-20f, 20f),
                    0,
                    Random.Range(0, 50)
                );

                Ship ship = new EnemyBuilder<Ship>(shipPrefab)
                    .Build(
                        MaxHP: 5 + level,
                        speed: Random.Range(1, 4),
                        position: randomPosition,
                        rotation: Quaternion.identity,
                        scale: Vector3.one * (1 + level / 2f)
                    );
            }
        }

        private void SpawnPlanes()
        {
            for (int i = 0; i < enemyQuantity * 2; i++)
            {
                Vector3 randomPosition = new Vector3(
                    player.transform.position.x + Random.Range(-30f, 30f),
                    0,
                    Random.Range(0, 50)
                );

                Plane plane = new EnemyBuilder<Plane>(planePrefab)
                    .Build(
                        MaxHP: 15 + level,
                        speed: 4 + level / 3f,
                        position: randomPosition,
                        rotation: Quaternion.identity,
                        scale: Vector3.one * (1 + level / 3f)
                    );
            }
        }
    }
}



