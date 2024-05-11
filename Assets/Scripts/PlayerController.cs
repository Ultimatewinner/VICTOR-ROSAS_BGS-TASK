using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float movementSpeed;
    public float damping;
    public Vector2 forceToApply;
    public Vector2 playerInput;

    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        Vector2 moveForce = playerInput * movementSpeed;

        moveForce += forceToApply;

        forceToApply /= damping;

        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }

        rigidbody.velocity = moveForce;
    }

}