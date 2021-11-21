using UnityEngine;

public class EnemySine : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 3.0f;

    [SerializeField]
    private float frequency = 3.0f;

    private Rigidbody2D _rigidbody;

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

        _rigidbody.velocity = _rigidbody.velocity.y  * Vector3.up + Vector3.right * Mathf.Sin(Time.time * frequency) * amplitude;
    }
}
