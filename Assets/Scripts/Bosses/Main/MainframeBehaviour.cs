using UnityEngine;
using System.Collections;

public class MainframeBehaviour : BossBehaviour
{
    [SerializeField]
    private BossScriptable bossAttributes;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject shields;

    protected override void Awake()
    {
        base.Awake();
        AddShieldBreakListeners();
        OnShieldDestroyed();
    }

    private void AddShieldBreakListeners()
    {
        // 1 - First stage shields need to be broken
        foreach(EnemyDamage enemyDamage in shields.GetComponentsInChildren<EnemyDamage>())
        {
            enemyDamage.AddOnEnemyDieListener(OnShieldDestroyed);
        }
    }

    private void OnShieldDestroyed()
    {
        // 2 - second stage arms attack and horizontal movement
        if(shields.transform.childCount < 2)
        {

        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1f, bossAttributes.attackWaitMaxSeconds));

        int attackType = Random.Range(0, bossAttributes.attackClasses.Length);
        BossAttackScriptable attack = bossAttributes.attackClasses[attackType];

        if (attack.name.Contains("Punch"))
        {
            animator.enabled = false;
        }

        Instantiate(attack.prefab, transform.position + Vector3.up * -2.5f, Quaternion.identity);

        if (attack.name.Contains("Punch"))
        {
            yield return new WaitForSeconds(0.5f);
            animator.enabled = true;
        }



        if (IsAlive)
        {
            StartCoroutine(Attack());
        } else
        {
            animator.enabled = false;
        }
    }
}
