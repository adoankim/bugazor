using UnityEngine;

enum MovementType
{
    Sine,
    Triangle,
    SawTooth,
}

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 3.0f;

    [SerializeField]
    private float frequency = 3.0f;

    [SerializeField]
    private MovementType movementType = MovementType.Sine;

    private Rigidbody2D _rigidbody;
    private float velocityX;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Enemy died and body is static now
        if (_rigidbody.bodyType.Equals(RigidbodyType2D.Static))
        {
            return;
        }

        switch (movementType)
        {
            case MovementType.Sine:
                velocityX = Mathf.Sin(Time.time * frequency) * amplitude;
                break;
            case MovementType.Triangle:
                velocityX = Mathf.Sign(Mathf.Sin(Time.time * frequency) * amplitude);
                break;
            case MovementType.SawTooth:
                velocityX = 1.0f - 2.0f * Mathf.Round(Mathf.Sin(Time.time * frequency) * amplitude);
                break;
        }

        _rigidbody.velocity = _rigidbody.velocity.y * Vector3.up + Vector3.right * velocityX;


    }
}
