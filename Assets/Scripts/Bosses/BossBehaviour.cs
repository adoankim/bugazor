using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

    private EnemyDamage enemyDamage;

    protected virtual void Awake()
    {
        enemyDamage = GetComponentInParent<EnemyDamage>();
        enemyDamage.AddOnEnemyDieListener(OnBossDied);
    }

    private void OnBossDied()
    {
        LevelsManager._instance.GoNextLevel();
    }

    protected bool IsAlive {
        get { 
            return enemyDamage.IsAlive; 
        }
    }

}
