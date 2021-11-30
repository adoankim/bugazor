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
    }

    private void OnLeftHandDestroyed()
    {
        leftHandDestroyed = true;
    }

    private void OnRightHandDestroyed()
    {
        rightHandDestroyed = true;
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
            StartCoroutine(AttackStage2());
        }
    }

    IEnumerator AttackStage2()
    {
        yield return new WaitForSeconds(Random.Range(1f, bossAttributes.attackWaitMaxSeconds));

        if (rightHandDestroyed && leftHandDestroyed)
        {
            yield return null;
        }

        bool isLeftAttack = Random.Range(0, 10) % 2 == 0;

        isLeftAttack = !leftHandDestroyed && (rightHandDestroyed || isLeftAttack);

        if (isLeftAttack)
        {
            leftHandAnimator.Play("MainLeftHandAttack");
        } 
        else if(!rightHandDestroyed)
        {

            rightHandAnimator.Play("MainRightHandAttack");
        }

        // The attack animations are 2 seconds long
        yield return new WaitForSeconds(2.0f);

        if (IsAlive)
        {
            StartCoroutine(AttackStage2());
        }
    }

    IEnumerator AttackStage3()
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
            StartCoroutine(AttackStage3());
        } else
        {
            animator.enabled = false;
        }
    }
}
