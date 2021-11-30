using UnityEngine;
using System.Collections;

public class MainframeBehaviour : BossBehaviour
{
    private static string stage2CoroutineName = "AttackStage2";

    [SerializeField]
    private BossScriptable bossAttributes;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject shields;

    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject rightHand;

    private Animator leftHandAnimator;
    private Animator rightHandAnimator;
    private bool leftHandDestroyed;
    private bool rightHandDestroyed;

    protected override void Awake()
    {
        base.Awake();

        AddShieldBreakListeners();
        SetupArms();
    }

    private void SetupArms()
    {
        leftHandAnimator = leftHand.GetComponent<Animator>();
        rightHandAnimator = rightHand.GetComponent<Animator>();

        leftHand.GetComponent<EnemyDamage>().AddOnEnemyDieListener(OnLeftHandDestroyed);
        rightHand.GetComponent<EnemyDamage>().AddOnEnemyDieListener(OnRightHandDestroyed);
        animator.enabled = true;
        StartCoroutine(stage2CoroutineName);
    }

    private void OnLeftHandDestroyed()
    {
        leftHandDestroyed = true;
        enemyDamage.ReceiveDamage(100);
        if (rightHandDestroyed)
        {
            StopCoroutine(stage2CoroutineName);
            animator.SetBool("isStage3", true);
            StartCoroutine(AttackStage3());
        }
    }

    private void OnRightHandDestroyed()
    {
        rightHandDestroyed = true;
        enemyDamage.ReceiveDamage(100);
        if (leftHandDestroyed)
        {
            StopCoroutine(stage2CoroutineName);
            animator.SetBool("isStage3", true);
            StartCoroutine(AttackStage3());
        }
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
            animator.enabled = true;
            StartCoroutine(stage2CoroutineName);
        }
    }

    private bool IsStage2Finished()
    {
        return rightHandDestroyed && leftHandDestroyed;
    }

    IEnumerator AttackStage2()
    {
        
        if (IsStage2Finished())
        {
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(1.5f, bossAttributes.attackWaitMaxSeconds));

        if (IsStage2Finished())
        {
            yield return null;
        }


        bool isLeftAttack = Random.Range(0, 10) % 2 == 0;

        isLeftAttack = !leftHandDestroyed && (rightHandDestroyed || isLeftAttack);
        animator.enabled = false;
        if (isLeftAttack)
        {
            leftHandAnimator.Play("MainLeftHandAttack");
        } 
        else if(!rightHandDestroyed)
        {

            rightHandAnimator.Play("MainRightHandAttack");
        }

        // The attack animations are 2 seconds long
        yield return new WaitForSeconds(1.5f);
        animator.enabled = true;

        if (IsAlive && !IsStage2Finished())
        {
            StartCoroutine(stage2CoroutineName);
        }
    }

    IEnumerator AttackStage3()
    {
        yield return new WaitForSeconds(Random.Range(1f, bossAttributes.attackWaitMaxSeconds));

        int attackType = Random.Range(0, bossAttributes.attackClasses.Length);
        BossAttackScriptable attack = bossAttributes.attackClasses[attackType];


        Instantiate(attack.prefab, transform.position + Vector3.up * -2.5f, Quaternion.identity);

        if (IsAlive)
        {
            StartCoroutine(AttackStage3());
        }
    }
}
