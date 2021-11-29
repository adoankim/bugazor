using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform leftSpawner;

    [SerializeField]
    private Transform centerSpawner;

    [SerializeField]
    private Transform rightSpawner;

    private Transform enemySpawnOrigin;

    public void SpawnEnemy(GameObject enemyPrefab, bool forceCenterSpawn = false)
    {
        if (forceCenterSpawn)
        {
            enemySpawnOrigin = centerSpawner;
        }
        else
        {
            int enemySpawnPosition = Random.Range(0, 3);
            switch (enemySpawnPosition)
            {
                case 0:
                    enemySpawnOrigin = leftSpawner;
                    break;
                case 1:
                    enemySpawnOrigin = centerSpawner;
                    break;
                case 2:
                    enemySpawnOrigin = rightSpawner;
                    break;
            }
        }
        Instantiate(enemyPrefab, enemySpawnOrigin.localPosition, Quaternion.identity, enemySpawnOrigin);
    }
}
