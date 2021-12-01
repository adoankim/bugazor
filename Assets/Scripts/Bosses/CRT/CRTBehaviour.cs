using UnityEngine;
using System.Collections;

public class CRTBehaviour : BossBehaviour
{
    [SerializeField]
    private float magnitude = 10f;

    [SerializeField]
    private BossScriptable bossAttributes;

    private int direction = -1;
    private bool ready = false;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Prepare());
    }

    IEnumerator Prepare()
    {
        while (!check.HasReachedTargetPosition)
        {
            yield return new WaitForSeconds(1.5f);
        }
        movement.enabled = true;
        _collider.enabled = true;
        ready = true;

        if (MusicManager._instance != null)
        {
            MusicManager._instance.PlayMidBossTheme();
        }

        yield return StartCoroutine(UpdateDirection());
        yield return new WaitForSeconds(.75f);
        yield return StartCoroutine("Attack");
    }

    void Update()
    {
        if (ready)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * magnitude * direction);
        }
    }

    IEnumerator UpdateDirection()
    {
        yield return new WaitForSeconds(2.5f);

        direction *= -1;

        StartCoroutine(UpdateDirection());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(1f, bossAttributes.attackWaitMaxSeconds));


        Instantiate(bossAttributes.attackClasses[0].prefab, transform.position + Vector3.up * -5.5f, Quaternion.identity);

        if (IsAlive)
        {
            StartCoroutine(Attack());
        }
    }
}
