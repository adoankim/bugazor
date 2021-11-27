using System.Collections;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    [Range(1.0f, 5.0f)]
    private float attackWaitMinSeconds = 1;

    [SerializeField]
    [Range(1.0f, 5.0f)]
    private float attackWaitMaxSeconds = 1;

    [SerializeField]
    private float distanceFromBody = -2.5f;

    void Start()
    {
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(attackWaitMinSeconds, attackWaitMinSeconds + attackWaitMaxSeconds));

        Instantiate(prefab, transform.position + Vector3.up * distanceFromBody, Quaternion.identity);
        StartCoroutine(Attack());
    }
}
