using UnityEngine;
using System.Collections;

public class CRTBehaviour : MonoBehaviour
{
    [SerializeField]
    private float magnitude = 10f;

    [SerializeField]
    private BossScriptable bossAttributes;

    private int direction = -1;

    private void Awake()
    {
        StartCoroutine(UpdateDirection());
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
}
