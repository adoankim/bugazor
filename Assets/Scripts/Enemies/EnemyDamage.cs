using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 2;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D boxCollider;

    private bool canTakeDamage = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ReceiveDamage(int damage)
    {
        if (!canTakeDamage)
        {
            return;
        }


        if (enemyHealth - damage >= 0)
        {
            enemyHealth -= damage;
            Debug.Log("Damage received: " + enemyHealth );
        }
        else
        {
            Debug.Log("Enemy died received: " + enemyHealth);
            canTakeDamage = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        boxCollider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        
        // TODO: Play die animation/effect

        yield return new WaitForSeconds(1.2f);

        Destroy(gameObject);
    }
}
