using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private void Start()
    {
        StartCoroutine(DestroyBulletAfter(5f));
    }

    private IEnumerator DestroyBulletAfter(float v)
    {
        yield return new WaitForSeconds(v);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position = (Vector3.right * transform.position.x) + Vector3.up * (transform.position.y + speed * Time.deltaTime);
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
