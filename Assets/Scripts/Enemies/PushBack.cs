using UnityEngine;

public class PushBack : MonoBehaviour
{
    [SerializeField]
    private float pushBackIntensity = 1.0f;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void DoPushBack(float multiplier)
    {
        _rigidbody.AddForce(Vector2.up * pushBackIntensity * multiplier, ForceMode2D.Impulse);
    }
}
