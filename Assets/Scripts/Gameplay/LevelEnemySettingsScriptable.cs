using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEnemySettingsScriptable", menuName = "ScriptableObjects/LevelEnemySettingsScriptable", order = 3)]
public class LevelEnemySettingsScriptable : ScriptableObject
{
    public List<GameObject> enemyPrefabs;
    public List<int> timeLapseMilestones;
    public GameObject bossPrefab;
    public Vector3 bossOffsetPosition;
    public float maxSpawnWait = 2.5f;
}
