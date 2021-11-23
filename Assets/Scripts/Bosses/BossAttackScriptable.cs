using UnityEngine;

[CreateAssetMenu(fileName = "BossAttackClass", menuName = "ScriptableObjects/BossAttackClass", order = 2)]
public class BossAttackScriptable : ScriptableObject
{
    public string attackName;

    public int multiplier;
    public int damage;
}