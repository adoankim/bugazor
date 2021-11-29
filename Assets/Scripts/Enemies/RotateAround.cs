using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Vector3 axis;

    [SerializeField]
    private bool randomStart = false;

    [SerializeField]
    private float startRange = 4.0f;

    private void Start()
    {
        if(axis == Vector3.zero)
        {
            axis = Vector3.forward;
        }

        if (randomStart)
        {
            target.transform.Translate(
                new Vector3(
                    Random.Range(-startRange, startRange),
                    Random.Range(-startRange, startRange), 
                    Random.Range(-startRange, startRange)
                )
            );
        }
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        target.transform.RotateAround(this.transform.position, axis, Time.deltaTime * speed);
    }
}
