using UnityEngine;
using System.Collections;

public class MobileBehaviour : BossBehaviour
{
    [SerializeField]
    private BossScriptable bossAttributes;

    [SerializeField]
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1f, bossAttributes.attackWaitMaxSeconds));

        int attackType = Random.Range(0, bossAttributes.attackClasses.Length);
        BossAttackScriptable attack = bossAttributes.attackClasses[attackType];

        if (attack.name.Contains("Podus"))
        {
            animator.enabled = false;
        }

        Instantiate(attack.prefab, transform.position + Vector3.up * -2.5f, Quaternion.identity);

        if (attack.name.Contains("Podus"))
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