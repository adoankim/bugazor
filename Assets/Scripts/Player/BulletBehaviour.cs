using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float pushBackMultiplier = 1f;

    [SerializeField]
    private GameObject particlePrefab;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Collider2D _collider;

    private void Start()
    {
        if(AudioManager._instance != null)
        {
            AudioManager._instance.PlayLaserSound();
        }

        StartCoroutine(DestroyBulletAfter(.85f));
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

            if (AudioManager._instance != null)
            {
                AudioManager._instance.PlayLaserImpactSound();
            }

            EnemyDamage enemyDamage = collision.gameObject.GetComponent<EnemyDamage>();
            if (enemyDamage != null)
            {
                collision.gameObject.GetComponent<EnemyDamage>().ReceiveDamage(1);
            }
            _collider.enabled = false;
            Instantiate(particlePrefab, transform.position, Quaternion.identity, transform);
            spriteRenderer.enabled = false;
            StartCoroutine(DestroyBulletAfter(1f));
        }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
