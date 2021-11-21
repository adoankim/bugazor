using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float pushBackIntensity = 3f;

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
            collision.gameObject.GetComponent<PushBack>().DoPushBack(pushBackIntensity);
            StartCoroutine(DestroyBulletAfter(0f));
        }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
