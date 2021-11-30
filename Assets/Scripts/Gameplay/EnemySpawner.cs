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

    public void SpawnEnemy(GameObject enemyPrefab, Vector3? offsetPosition = null, bool forceCenterSpawn = false)
    {
        if (offsetPosition == null)
        {
            offsetPosition = Vector3.zero;
        }

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

                    if (enemyPrefab.GetComponent<CrossScreenMovement>() == null)
                    {
                        enemySpawnOrigin = centerSpawner;
                    } else
                    {
                        enemySpawnOrigin = rightSpawner;
                    }
                    break;
                case 2:
                    enemySpawnOrigin = rightSpawner;
                    break;
            }
        }
        Instantiate(enemyPrefab, enemySpawnOrigin.localPosition + offsetPosition.Value, Quaternion.identity, enemySpawnOrigin);
    }
}
