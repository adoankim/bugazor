using UnityEngine;

public class MoveVerticalFromAToB : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float startPoint;

    [SerializeField]
    private float endPoint;

    private Vector3 basePoint;
    private bool reachTarget;

    void Start()
    {
        basePoint = gameObject.transform.position.x * Vector3.right + gameObject.transform.position.z * Vector3.forward;
        gameObject.transform.position = basePoint  + Vector3.up * startPoint;
    }

    void Update()
    {
        if (reachTarget)
        {
            return;
        }

        if (Mathf.Abs(gameObject.transform.position.y - endPoint) < 0.1f)
        {
            reachTarget = true;
            return;
        }

        gameObject.transform.position = basePoint + Vector3.up * (gameObject.transform.position.y - Time.deltaTime * speed);

    }
}
