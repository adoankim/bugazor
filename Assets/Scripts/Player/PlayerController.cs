using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float boost = 2.0f;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    public void StopMove()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        bool withBoost = Input.GetKey(KeyCode.LeftShift);

        if (h > 0 && transform.position.x < 18)
        {
            _rigidbody.velocity = new Vector2(speed * (withBoost ? boost : 1), _rigidbody.velocity.y);
        }
        else if (h < 0 && transform.position.x > -18)
        {
            _rigidbody.velocity = new Vector2(-speed * (withBoost ? boost : 1), _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }
}
