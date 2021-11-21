using UnityEngine;

public class EnemyDamageDeal : MonoBehaviour
{
    [SerializeField]
    private int damagePoints = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            collision.gameObject.GetComponent<PlayerDamage>().ReceiveDamage(damagePoints);
        }   
    }
}
