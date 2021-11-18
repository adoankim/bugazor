using UnityEngine;

public class PlayerSine : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 3.0f;

    [SerializeField]
    private float frequency = 3.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
    }
}
