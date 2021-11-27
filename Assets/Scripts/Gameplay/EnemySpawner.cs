using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform leftSpawner;

    [SerializeField]
    private Transform centerSpawner;

    [SerializeField]
    private Transform rightSpawner;

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    private static string coroutineName = "SpawnEnemy";
    private Transform enemySpawnOrigin;

    private void OnEnable()
    {
        StartCoroutine(coroutineName);
    }

    private void OnDisable()
    {
        StopCoroutine(coroutineName);   
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(.5f, 2.5f));
        int enemyType = Random.Range(0, enemyPrefabs.Count);
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
        Instantiate(enemyPrefabs[enemyType], enemySpawnOrigin.localPosition, Quaternion.identity, enemySpawnOrigin);

        StartCoroutine(coroutineName);
    }
}
