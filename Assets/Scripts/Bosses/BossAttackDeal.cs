using UnityEngine;

public class BossAttackDeal : MonoBehaviour
{

    [SerializeField]
    BossAttackScriptable attackAttributes;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(Tags.Player))
        {
            collision.gameObject.GetComponent<PlayerDamage>().ReceiveDamage(attackAttributes.damage);
            Destroy(gameObject);
        }
    }
}
