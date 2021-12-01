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
    private PlayerShoot playerShoot;

    [SerializeField]
    private List<LevelEnemySettingsScriptable> levelsSettings;

    [SerializeField]
    private LevelEnemySettingsScriptable finalBossLevelSetting;

    private bool isBossFight = false;
    private bool isFinalBossFightStarted = false;
    private int currentLevel = 0;
    private int currentLevelStage = 0;
    private float playTime = 0.0f;
    private float totalPlayTime = 0.0f;
    List<GameObject> currentEnemyPrefabs;
    private LevelEnemySettingsScriptable currentLevelSettings;

    private void OnEnable()
    {
        _instance = this;
        currentLevelSettings = levelsSettings[currentLevel];
        currentEnemyPrefabs = currentLevelSettings.enemyPrefabs;

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
        if (isBossFight || isFinalBossFightStarted)
        {
            return;
        }

        // Skip if level stage milestone not reached
        if (currentLevelSettings.timeLapseMilestones[currentLevelStage] - playTime > 0)
        {
            return;
        }


        totalPlayTime = totalPlayTime + playTime;
        playTime = 0;

        int bossStage = currentLevelSettings.timeLapseMilestones.Count;
        currentLevelStage = Mathf.Clamp(currentLevelStage + 1, 0, bossStage);
        // If new level stage is boss stage, start boss fight
        if (currentLevelStage == bossStage)
        {
            isBossFight = true;
            StopCoroutine(coroutineName);
            StartCoroutine(SpawnBoss(currentLevelSettings.bossPrefab, currentLevelSettings.bossOffsetPosition));
        }
    }

    public void GoNextLevel()
    {
        int nextLevel = currentLevel + 1;
 
        if (MusicManager._instance != null)
        {
            MusicManager._instance.PlayMainTheme();
        }

        if ( nextLevel < levelsSettings.Count)
        {
            playerShoot.IncreaseShootSpeed();
            currentLevel = nextLevel;
            totalPlayTime = totalPlayTime + playTime;
            playTime = 0;
            currentLevelStage = 0;
            currentLevelSettings = levelsSettings[currentLevel];
            isBossFight = false;
            currentEnemyPrefabs = currentLevelSettings.enemyPrefabs;
            StartCoroutine(StartNextLevel());
        } else if(!isFinalBossFightStarted)
        {
            isFinalBossFightStarted = true;
            StopCoroutine(coroutineName);
            StartCoroutine(SpawnFinalBoss());
        }
    }

    IEnumerator SpawnFinalBoss()
    {
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(SpawnBoss(finalBossLevelSetting.bossPrefab, finalBossLevelSetting.bossOffsetPosition));
    }
    IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(coroutineName);
    }

    IEnumerator SpawnBoss(GameObject bossPrefab, Vector3 bossOffsetPosition)
    {
        while(enemySpawner.NumberOfSpawnedEnemies > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        enemySpawner.SpawnEnemy(bossPrefab, bossOffsetPosition, true);
    }

    IEnumerator ManageLevel()
    {
        yield return new WaitForSeconds(Random.Range(.5f, currentLevelSettings.maxSpawnWait));

        // Stage clamp increases after reaching every time lapse milestone
        // This way we show different enemy types after some time passed.
        int enemyTypeStageClamp = Mathf.Clamp(currentLevelStage + 1, 1, currentEnemyPrefabs.Count);
        int enemyType = Random.Range(0, enemyTypeStageClamp);

        enemySpawner.SpawnEnemy(currentEnemyPrefabs[enemyType]);

        StartCoroutine(coroutineName);
    }

}
