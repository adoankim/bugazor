using UnityEngine;

public class DisketteAttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private float magnitude = 100f;

    void Update()
    {

        transform.RotateAround(transform.localPosition, Vector3.back, Time.deltaTime * magnitude);
    }

}
