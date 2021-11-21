using UnityEngine;

public class DestroyEnemiesOutsideMap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Tags.IsEnemyTag(collision.tag))
        {
            Destroy(collision.gameObject);
        }
    }
}
