using UnityEngine;

public class BossBehaviour : MonoBehaviour
{

    protected EnemyDamage enemyDamage;

    [SerializeField]
    protected MoveVerticalFromAToB check;

    [SerializeField]
    protected EnemyMovement movement;

    [SerializeField]
    protected Collider2D _collider;

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
