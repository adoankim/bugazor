using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float pushBackMultiplier = 1f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Tags.IsEnemyTag(collision.tag))
        {
            PushBack pushBack = collision.gameObject.GetComponent<PushBack>();
            if (pushBack != null)
            {
                pushBack.DoPushBack(pushBackMultiplier);
            }
            collision.gameObject.GetComponent<EnemyDamage>().ReceiveDamage(1);
            StartCoroutine(DestroyBulletAfter(0f));
        }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
