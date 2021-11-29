using UnityEngine;

public class CrossScreenMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;

    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        float randomSpeed = Random.Range(speed / 2, speed);
        float xForce = randomSpeed;
        if (transform.position.x >= 0)
        {
            xForce *= -1;
        }

        _rigidbody.AddForce(new Vector2(xForce, -randomSpeed), ForceMode2D.Impulse);
    }

}
