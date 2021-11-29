using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private static string coroutineName = "ManageLevel";
    public static LevelsManager _instance;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private List<LevelEnemySettingsScriptable> levelsSettings;

    private bool isBossFight = false;
    private int currentLevel = 0;
    private int currentLevelStage = 0;
    private float playTime = 0.0f;
    private float totalPlayTime = 0.0f;
    List<GameObject> currentEnemyPrefabs;
    private LevelEnemySettingsScriptable currentLevelSettings;

    private void OnEnable()
    {
        _instance = this;
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
            UpdatePlaytimeUI();
            CheckNextLevelAndUpdate();
            
        }
    }

    private void UpdatePlaytimeUI()
    {
        // TODO: Update playtime UI
    }

    private void CheckNextLevelAndUpdate()
    {
        // Skip level check during bossfights
        if (isBossFight)
        {
            return;
        }

        // Skip if level stage milestone not reached
        if (currentLevelSettings.timeLapseMilestones[currentLevelStage] - playTime > 0)
        {
            return;
        }


        int bossStage = currentLevelSettings.timeLapseMilestones.Count;
        currentLevelStage = Mathf.Clamp(currentLevelStage + 1, 0, bossStage);
        // If new level stage is boss stage, start boss fight
        if (currentLevelStage == bossStage)
        {
            isBossFight = true;
            StopCoroutine(coroutineName);
            enemySpawner.SpawnEnemy(currentLevelSettings.bossPrefab, currentLevelSettings.bossOffsetPosition, true);
        }
    }

    public void GoNextLevel()
    {
        int nextLevel = currentLevel + 1;
        if ( nextLevel < levelsSettings.Count)
        {
            currentLevel = nextLevel;
            totalPlayTime = totalPlayTime + playTime;
            playTime = 0;
            currentLevelStage = 0;
            currentLevelSettings = levelsSettings[currentLevel];
            isBossFight = false;
            currentEnemyPrefabs = currentLevelSettings.enemyPrefabs;
            StartCoroutine(coroutineName);
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
