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

        private float spawnInterval = 20f;
        public DistanceTracker distanceTracker;
        private float nextSpawnTime;

        private void FixedUpdate()
        {
            float dist = (float)DistanceTracker.DistanceCovered;

            spawnInterval = Mathf.Clamp(20f - dist / 50f, 0.5f, 5f);

            if (Time.time >= nextSpawnTime)
            {
                Spawn();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        private void Spawn()
        {
            if (player == null) return;

            float dist = (float)DistanceTracker.DistanceCovered;

            enemyQuantity = Mathf.Clamp(Mathf.FloorToInt(2 + dist / 20f), 2, 100);
            level = Mathf.Clamp(Mathf.FloorToInt(dist / 30f), 1, 50);

            Debug.Log($"Spawning {enemyQuantity} enemigos. Nivel: {level}");

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

                float scaleFactor = Mathf.Clamp(1f + level * 0.2f, 1f, 5f);

                Ship ship = new EnemyBuilder<Ship>(shipPrefab)
                    .Build(
                        MaxHP: 5 + level,
                        speed: Random.Range(1, 4),
                        position: randomPosition,
                        rotation: Quaternion.identity,
                        scale: Vector3.one * scaleFactor
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

                float scaleFactor = Mathf.Clamp(1f + level * 0.2f, 1f, 5f);

                Plane plane = new EnemyBuilder<Plane>(planePrefab)
                    .Build(
                        MaxHP: 15 + level,
                        speed: 4 + level / 3f,
                        position: randomPosition,
                        rotation: Quaternion.identity,
                        scale: Vector3.one * scaleFactor
                    );
            }
        }
    }
}



