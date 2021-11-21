using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DamageReceived : UnityEvent<int>
{
}

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private int playerHealth = 10;

    [SerializeField]
    private float timeBetweenDamage = 2f;

    private bool canTakeDamage = true;

    private DamageReceived onDamageReceived = new DamageReceived();

    public int PlayerHealth
    {
        get {
            return playerHealth;
        }
    }

    private void OnDestroy()
    {
        onDamageReceived.RemoveAllListeners();
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
        onDamageReceived.Invoke(playerHealth);

        if (playerHealth - damage >= 0)
        {
            playerHealth -= damage;
        } 
        else
        {
            // Game over condition
        }

        onDamageReceived.Invoke(playerHealth);
    }

    IEnumerator DamageTimeout(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        canTakeDamage = true;
    }

    public void AddOnDamageDealtListener(UnityEngine.Events.UnityAction<int> listener)
    {
        onDamageReceived.AddListener(listener);
    }

    public void RemoveOnDamageDealtListener(UnityEngine.Events.UnityAction<int> listener)
    {
        onDamageReceived.RemoveListener(listener);
    }
}
