using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthChanged : UnityEvent<int>
{
}

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private int playerHealth = 15;

    [SerializeField]
    private int playerMaxHealth = 15;

    [SerializeField]
    private float timeBetweenDamage = 2f;

    private bool canTakeDamage = true;

    private HealthChanged onHealthChanged = new HealthChanged();

    public int PlayerHealth
    {
        get {
            return playerHealth;
        }
    }

    private void OnDestroy()
    {
        onHealthChanged.RemoveAllListeners();
    }

    public void AddLives(int lives)
    {
        int newHealth = playerHealth + lives;
        playerHealth = Mathf.Clamp(newHealth, newHealth, playerMaxHealth);
        onHealthChanged.Invoke(playerHealth);
    }

    public void ReceiveDamage(int damage)
    {
        if(!canTakeDamage)
        {
            Debug.Log("Cannot take damage");
            return;
        }
        
        canTakeDamage = false;

        StartCoroutine(DamageTimeout(timeBetweenDamage));
        onHealthChanged.Invoke(playerHealth);

        if (playerHealth - damage >= 0)
        {
            playerHealth -= damage;
        } 
        else
        {
            // Game over condition
        }

        onHealthChanged.Invoke(playerHealth);
    }

    IEnumerator DamageTimeout(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        canTakeDamage = true;
    }

    public void AddOnHealthChangedListener(UnityEngine.Events.UnityAction<int> listener)
    {
        onHealthChanged.AddListener(listener);
    }

    public void RemoveOnHealthChangedListener(UnityEngine.Events.UnityAction<int> listener)
    {
        onHealthChanged.RemoveListener(listener);
    }
}
