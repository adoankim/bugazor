using UnityEngine;
using System.Collections;

public class MainframeBehaviour : BossBehaviour
{
    private static string stage2CoroutineName = "AttackStage2";
    private static string stage3CoroutineName = "AttackStage3";

    [SerializeField]
    private BossScriptable bossAttributes;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject shields;

    private MainframuStory mainframuStory;

    private GameObject leftHand;
    private GameObject rightHand;
    private Animator leftHandAnimator;
    private Animator rightHandAnimator;
    private bool leftHandDestroyed;
    private bool rightHandDestroyed;
    private bool allShieldsBroke;

    protected override void Awake()
    {
        base.Awake();

        mainframuStory = transform.parent.transform.parent.GetComponent<MainframuStory>();
        mainframuStory.AddOnStartStoryFinishedListener(StartStage1);

        SetupArms();
    }

    private void StartStage1()
    {
        StartCoroutine(CheckStage1());
    }

    protected override void OnBossDied()
    {
        animator.enabled = false;
        gameObject.transform.parent.transform.position = Vector3.right + gameObject.transform.parent.transform.position.y * Vector3.up;
        gameObject.transform.parent.transform.GetComponent<RotateAround>().enabled = true;
        animator.enabled = true;
        animator.SetBool("mainDied", true);
    }

    private void SetupArms()
    {

        leftHand = gameObject.transform.parent.Find("LeftHand").gameObject;
        rightHand = gameObject.transform.parent.Find("RightHand").gameObject;

        leftHandAnimator = leftHand.GetComponent<Animator>();
        rightHandAnimator = rightHand.GetComponent<Animator>();

        leftHand.GetComponent<EnemyDamage>().AddOnEnemyDieListener(OnLeftHandDestroyed);
        rightHand.GetComponent<EnemyDamage>().AddOnEnemyDieListener(OnRightHandDestroyed);
    }

    private void OnLeftHandDestroyed()
    {
        leftHandDestroyed = true;
        enemyDamage.ReceiveDamage(100);
        if (rightHandDestroyed)
        {
            StopCoroutine(stage2CoroutineName);
            animator.enabled = true;
            animator.SetBool("isStage3", true);
            StartCoroutine(stage3CoroutineName);
        }
    }

    private void OnRightHandDestroyed()
    {
        rightHandDestroyed = true;
        enemyDamage.ReceiveDamage(100);
        if (leftHandDestroyed)
        {
            StopCoroutine(stage2CoroutineName);
            animator.enabled = true;
            animator.SetBool("isStage3", true);
            StartCoroutine(stage3CoroutineName);
        }
    }

    private void OnShieldDestroyed()
    {
        if(!allShieldsBroke && shields.transform.childCount < 1)
        {
            // 2 - second stage arms attack and horizontal movement
            mainframuStory.OnShieldBroken(StartStage2);
            allShieldsBroke = true;
        }
    }

    IEnumerator CheckStage1()
    {
        int timelapsed = 0;
        while (!allShieldsBroke && timelapsed < 180)
        {
            yield return new WaitForSeconds(2);
            OnShieldDestroyed();
            timelapsed += 2;
        }

        if (!allShieldsBroke)
        {
            allShieldsBroke = true;
            Destroy(shields);
            mainframuStory.OnShieldBroken(StartStage2);
        }
    }

    private void StartStage2()
    {
        transform.GetComponent<Collider2D>().enabled = true;
        leftHand.GetComponent<Collider2D>().enabled = true;
        rightHand.GetComponent<Collider2D>().enabled = true;

        animator.enabled = true;
        StartCoroutine(stage2CoroutineName);
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

        yield return new WaitForSeconds(Random.Range(1f, 2f));

        if (IsStage2Finished())
        {
            yield return null;
        }


        bool isLeftAttack = Random.Range(0, 10) % 2 == 0;

        isLeftAttack = !leftHandDestroyed && (rightHandDestroyed || isLeftAttack);
        animator.enabled = false;
        bool attackStarted = true;
        if (isLeftAttack)
        {
            try {
                leftHandAnimator.Play("MainLeftHandAttack");
            } catch
            {
                attackStarted = false;
            }
        }
        else if (!rightHandDestroyed)
        {
            try
            {
                rightHandAnimator.Play("MainRightHandAttack");
            }
            catch
            {
                attackStarted = false;
            }
        }

        if (attackStarted)
        {
            // The attack animations are 2 seconds long
            yield return new WaitForSeconds(1.5f);
        }
        animator.enabled = true;

        if (IsAlive && !IsStage2Finished())
        {
            StartCoroutine(stage2CoroutineName);
        }
    }

    IEnumerator AttackStage3()
    {
        yield return new WaitForSeconds(Random.Range(.25f, 1.0f));

        int attackType = Random.Range(0, bossAttributes.attackClasses.Length);
        BossAttackScriptable attack = bossAttributes.attackClasses[attackType];

        if (IsAlive)
        {
            Instantiate(attack.prefab, transform.position + Vector3.up * -2.5f, Quaternion.identity);
            StartCoroutine(stage3CoroutineName);
        }
    }
}
