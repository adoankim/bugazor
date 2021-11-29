using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 2;

    [SerializeField]
    private int pointsPerDefeat = 1;

    [SerializeField]
    private float defeatTimeout = 1.2f;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    
    private Collider2D _collider;

    private bool canTakeDamage = true;

    private UnityEvent onEnemyDie= new UnityEvent();

    public bool IsAlive
    {
        get { return canTakeDamage;  }
    }

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        _collider = GetComponent<Collider2D>();
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

    private void OnDestroy()
    {
        onEnemyDie.RemoveAllListeners();
    }

    public void AddOnEnemyDieListener(UnityEngine.Events.UnityAction listener)
    {
        onEnemyDie.AddListener(listener);
    }

    IEnumerator Die()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        _rigidbody.bodyType = RigidbodyType2D.Static;

        // TODO: Play die animation/effect

        yield return new WaitForSeconds(defeatTimeout);

        GameplayManager.playerPointsRef.Add(pointsPerDefeat);
        onEnemyDie.Invoke();

        Destroy(gameObject);
    }
}
