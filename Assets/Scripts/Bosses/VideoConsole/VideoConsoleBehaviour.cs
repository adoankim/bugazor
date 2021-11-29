using UnityEngine;
using System.Collections;

public class VideoConsoleBehaviour : BossBehaviour
{
    [SerializeField]
    private float magnitude = 10f;

    [SerializeField]
    private BossScriptable bossAttributes;

    private int direction = -1;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(UpdateDirection());
        StartCoroutine("Attack");
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * magnitude * direction);
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


        Instantiate(bossAttributes.attackClasses[0].prefab, transform.position + Vector3.up * -2.5f, Quaternion.identity);

        if (IsAlive)
        {
            StartCoroutine(Attack());
        }
    }
}
