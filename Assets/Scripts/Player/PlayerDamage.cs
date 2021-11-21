using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private int playerHealth = 10;

    [SerializeField]
    private float timeBetweenDamage = 2f;

    private bool canTakeDamage = true;

    public void ReceiveDamage(int damage)
    {
        if(!canTakeDamage)
        {
            Debug.Log("Cannot take damage");
            return;
        }
        
        canTakeDamage = false;

        StartCoroutine(DamageTimeout(timeBetweenDamage));
        
        if (playerHealth - damage >= 0)
        {
            playerHealth -= damage;
        } 
        else
        {
            // Game over condition
        }
    }

    IEnumerator DamageTimeout(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        canTakeDamage = true;
    }
}
