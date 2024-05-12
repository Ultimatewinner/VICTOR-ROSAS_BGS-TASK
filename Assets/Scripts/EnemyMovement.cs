using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 2f;
    private Rigidbody2D rigidbody;
    private Vector2 moveDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (moveDirection != Vector2.zero)
        {
            rigidbody.MovePosition(rigidbody.position + moveDirection * (movementSpeed * Time.fixedDeltaTime));
        }
    }

    public void TranslateTowards(Vector2 targetPos)
    {
        moveDirection = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;
    }
}