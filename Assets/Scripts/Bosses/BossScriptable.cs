using UnityEngine;

[CreateAssetMenu(fileName = "BossClass", menuName = "ScriptableObjects/BossClass", order = 1)]
public class BossScriptable : ScriptableObject
{
    public string bossName;

    public GameObject prefab;

    public BossAttackScriptable[] attackClasses;
}