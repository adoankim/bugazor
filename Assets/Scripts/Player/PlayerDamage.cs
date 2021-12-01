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
    private ParticleSystem damageParticle;

    [SerializeField]
    private ParticleSystem healParticles;

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
        healParticles.Play();
        int newHealth = playerHealth + lives;
        playerHealth = Mathf.Clamp(newHealth, newHealth, playerMaxHealth);
        onHealthChanged.Invoke(playerHealth);
    }

    public void ReceiveDamage(int damage)
    {
        if(!canTakeDamage)
        {
            return;
        }
        
        canTakeDamage = false;

        StartCoroutine(DamageTimeout(timeBetweenDamage));
        onHealthChanged.Invoke(playerHealth);
        damageParticle.Play();
        if (playerHealth - damage >= 0)
        {
            if (AudioManager._instance != null)
            {
                AudioManager._instance.PlayPlayerImpactSound();
            }
            playerHealth -= damage;
        } 
        else
        {
            playerHealth = 0;
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
