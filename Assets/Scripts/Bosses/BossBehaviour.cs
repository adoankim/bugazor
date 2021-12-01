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

    [SerializeField]
    protected GameObject chiefPrefab;

    protected virtual void Awake()
    {
        enemyDamage = GetComponentInParent<EnemyDamage>();
        enemyDamage.AddOnEnemyDieListener(OnBossDied);
        enemyDamage.AddOnDamageTaken(OnDamageTaken);
    }

    public void ReleaseChief(int pos)
    {
        int kind = pos % chiefPrefab.transform.childCount;

        GameObject chief = Instantiate(chiefPrefab, transform.position, Quaternion.identity);
        chief.transform.GetChild(kind).gameObject.SetActive(true);
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
