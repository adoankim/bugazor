using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 2;

    [SerializeField]
    private int pointsPerDefeat = 1;

    [SerializeField]
    private float defeatTimeout = 1.2f;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D boxCollider;

    private bool canTakeDamage = true;

    public bool IsAlive
    {
        get { return canTakeDamage;  }
    }

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


        if (enemyHealth - damage > 0)
        {
            enemyHealth -= damage;
        }
        else
        {
            canTakeDamage = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        _rigidbody.bodyType = RigidbodyType2D.Static;

        // TODO: Play die animation/effect

        yield return new WaitForSeconds(defeatTimeout);

        GameplayManager.playerPointsRef.Add(pointsPerDefeat);

        Destroy(gameObject);
    }
}
