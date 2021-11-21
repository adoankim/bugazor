using UnityEngine;

public class PushBack : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void DoPushBack(float intensity)
    {
        _rigidbody.AddForce(Vector2.up * intensity, ForceMode2D.Impulse);
    }
}
