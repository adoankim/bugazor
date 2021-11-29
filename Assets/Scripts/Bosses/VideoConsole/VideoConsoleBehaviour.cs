using UnityEngine;
using System.Collections;

public class VideoConsoleBehaviour : BossBehaviour
{
    [SerializeField]
    private BossScriptable bossAttributes;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Animator innerAnimator;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine("Attack");
    }

    protected override void OnDamageTaken()
    {
        base.OnDamageTaken();

        if (IsAlive)
        {
            innerAnimator.Play("VideoConsoleScaleFlash");
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1f, bossAttributes.attackWaitMaxSeconds));


        Instantiate(bossAttributes.attackClasses[0].prefab, transform.position + Vector3.up * -2.5f, Quaternion.identity);

        

        if (IsAlive)
        {
            StartCoroutine(Attack());
        } else
        {
            animator.enabled = false;
        }
    }
}
