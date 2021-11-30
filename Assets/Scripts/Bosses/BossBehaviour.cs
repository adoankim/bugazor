using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

    protected EnemyDamage enemyDamage;

    protected virtual void Awake()
    {
        enemyDamage = GetComponentInParent<EnemyDamage>();
        enemyDamage.AddOnEnemyDieListener(OnBossDied);
        enemyDamage.AddOnDamageTaken(OnDamageTaken);
    }

    protected virtual void OnBossDied()
    {
        LevelsManager._instance.GoNextLevel();
    }
    protected virtual void OnDamageTaken()
    {
    }

    protected bool IsAlive {
        get { 
            return enemyDamage.IsAlive; 
        }
    }

}
