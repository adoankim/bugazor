using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed);    
    }
}
