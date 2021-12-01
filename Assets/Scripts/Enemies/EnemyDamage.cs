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

    [SerializeField]
    private bool destroyAfterDie = true;

    [SerializeField]
    private GameObject destroyFXPrefab;

    private Collider2D _collider;

    private bool canTakeDamage = true;

    private UnityEvent onEnemyDie = new UnityEvent();

    private UnityEvent onDamageTaken = new UnityEvent();

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

        onDamageTaken.Invoke();
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
        onDamageTaken.RemoveAllListeners();
    }

    public void AddOnEnemyDieListener(UnityEngine.Events.UnityAction listener)
    {
        onEnemyDie.AddListener(listener);
    }

    public void AddOnDamageTaken(UnityEngine.Events.UnityAction listener)
    {
        onDamageTaken.AddListener(listener);
    }

    IEnumerator Die()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        if (_rigidbody != null)
        {
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }

        if (AudioManager._instance != null)
        {
            AudioManager._instance.PlayEnemyExplodeSound();
        }

        if (destroyFXPrefab != null)
        {
            Instantiate(destroyFXPrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(defeatTimeout);

        GameplayManager.playerPointsRef.Add(pointsPerDefeat);
        onEnemyDie.Invoke();

        if (destroyAfterDie) { 
            Destroy(gameObject);
            if (_rigidbody != null)
            {
                Destroy(_rigidbody.gameObject);
            }
        }
    }
}
