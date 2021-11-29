using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private static string coroutineName = "ManageLevel";

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private List<LevelEnemySettingsScriptable> levelsSettings;

    private int currentLevel = 0;
    private float playTime = 0.0f;
    List<GameObject> enemyPrefabs;

    private void OnEnable()
    {
        StartCoroutine(coroutineName);
    }

    private void OnDisable()
    {
        StopCoroutine(coroutineName);
    }

    private void Update()
    {
        playTime = playTime + Time.deltaTime;
        if (playTime % 1 < 0.02f)
        {
            // TODO: Update playtime UI
        }
    }

    IEnumerator ManageLevel()
    {
        yield return new WaitForSeconds(Random.Range(.5f, 2.5f));

        enemyPrefabs = levelsSettings[currentLevel].enemyPrefabs;
        int enemyType = Random.Range(0, enemyPrefabs.Count);
        enemySpawner.SpawnEnemy(enemyPrefabs[enemyType]);

        StartCoroutine(coroutineName);
    }

}
