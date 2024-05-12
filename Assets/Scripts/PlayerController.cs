using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator anim;
    public SpriteRenderer playerRenderer;
    public float movementSpeed;
    public float damping;
    [HideInInspector]
    public Vector2 forceToApply;
    [HideInInspector]
    public Vector2 playerInput;
    [HideInInspector]
    public bool lastMoveNegative = false;
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

        if (moveForce.x > 0)
        {
            playerRenderer.flipX = false;
        }
        else if (0 > moveForce.x) 
        {
            playerRenderer.flipX = true;
        }

        if (moveForce.y > 0)
        {
            lastMoveNegative = true;
            anim.SetBool("lastMoveNegative", lastMoveNegative);
        }
        else if (0 > moveForce.y)
        {
            lastMoveNegative = false;
            anim.SetBool("lastMoveNegative", lastMoveNegative);
        }

        anim.SetFloat("speedX", moveForce.x);
        anim.SetFloat("speedY", moveForce.y);
    }

}