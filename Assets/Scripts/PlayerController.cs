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
    public float runSpeed;
    public OnHover onHover;
    public float runDamping;
    private float normalSpeed;
    private float normalDamping;
    [HideInInspector]
    public Vector2 forceToApply;
    [HideInInspector]
    public Vector2 playerInput;
    [HideInInspector]
    public bool lastMoveNegative = false;

    private void Awake()
    {
        normalSpeed = movementSpeed;
        normalDamping = damping;
    }

    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Run();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            onHover.inStore = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            onHover.inStore = false;
        }
    }

    void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = runSpeed;
            damping = runDamping;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = normalSpeed;
            damping = normalDamping;
        }
    }

}