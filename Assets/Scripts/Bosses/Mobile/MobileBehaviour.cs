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
        StartCoroutine(Prepare());
    }
    protected override void OnBossDied()
    {
        ReleaseChief(1);
        Destroy(transform.parent.gameObject);
        base.OnBossDied();
    }

    IEnumerator Prepare()
    {
        while (!check.HasReachedTargetPosition)
        {
            yield return new WaitForSeconds(1.5f);
        }
        _collider.enabled = true;
        animator.enabled = true;

        if (MusicManager._instance != null)
        {
            MusicManager._instance.PlayMidBossTheme();
        }

        yield return new WaitForSeconds(.75f);
        yield return StartCoroutine("Attack");
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
