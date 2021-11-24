using UnityEngine;

[CreateAssetMenu(fileName = "BossClass", menuName = "ScriptableObjects/BossClass", order = 1)]
public class BossScriptable : ScriptableObject
{
    public string bossName;

    public GameObject prefab;

    [Range(1, 4)]
    public float attackWaitMaxSeconds;

    public BossAttackScriptable[] attackClasses;
    
}