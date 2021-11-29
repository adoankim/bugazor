using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEnemySettingsScriptable", menuName = "ScriptableObjects/LevelEnemySettingsScriptable", order = 3)]
public class LevelEnemySettingsScriptable : ScriptableObject
{
    public List<GameObject> enemyPrefabs;
    public GameObject bossPrefab;
}
