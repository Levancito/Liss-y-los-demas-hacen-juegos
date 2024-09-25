using UnityEngine;

namespace Builder
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Enemy prefab;

        [SerializeField]
        private int level;

        private void Awake()
        {

            for (int i = 0; i < level; i++) 
            {
                //Enemy enemy = Instantiate(prefab, new Vector3(Random.Range(0, 50), 0, Random.Range(0, 50)), Quaternion.identity);
                //enemy.transform.localScale *= 1 + level / 2f;
                //enemy.MaxHealth = 25 + level * 5;
                //enemy.Color = new Color(Random.Range(0,1f), Random.Range(0,1f), Random.Range(0,1f), 1);
                //enemy.Speed = 2 + level / 3f;

               Enemy enemy =  new EnemyBuilder(prefab)
                    .SetPosition(new Vector3(Random.Range(0, 50), 0, Random.Range(0, 50)))
                    //.SetPosition(Random.Range(0, 50), 0, Random.Range(0,50))
                    .SetScale(1 + level / 2f)
                    .SetMaxHealth(25 + level * 5)
                    .SetSpeed(2 + level / 3f)
                    .Build();
            }
        }
    }
}
