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
    private int currentLevelStage = 0;
    private float playTime = 0.0f;
    List<GameObject> currentEnemyPrefabs;
    private LevelEnemySettingsScriptable currentLevelSettings;

    private void OnEnable()
    {
        StartCoroutine(coroutineName);
    }

    private void OnDisable()
    {
        StopCoroutine(coroutineName);
    }

    private void Start()
    {
        currentLevelSettings = levelsSettings[currentLevel];
        currentEnemyPrefabs = currentLevelSettings.enemyPrefabs;
    }

    private void Update()
    {
        playTime = playTime + Time.deltaTime;
        if (playTime % 1 < 0.02f)
        {
            // TODO: Update playtime UI

            if (currentLevelSettings.timeLapseMilestones[currentLevelStage] - playTime < 0)
            {
                currentLevelStage = Mathf.Clamp(currentLevelStage + 1, 0, currentLevelSettings.timeLapseMilestones.Count - 1);
            }
        }
    }

    IEnumerator ManageLevel()
    {
        yield return new WaitForSeconds(Random.Range(.5f, 2.5f));

        // Stage clamp increases after reaching every time lapse milestone
        // This way we show different enemy types after some time passed.
        int enemyTypeStageClamp = Mathf.Clamp(currentLevelStage + 1, 1, currentEnemyPrefabs.Count);
        int enemyType = Random.Range(0, enemyTypeStageClamp);

        enemySpawner.SpawnEnemy(currentEnemyPrefabs[enemyType]);

        StartCoroutine(coroutineName);
    }

}
