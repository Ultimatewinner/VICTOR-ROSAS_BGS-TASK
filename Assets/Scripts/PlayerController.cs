using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator anim;
    public SpriteRenderer playerRenderer;
    public SwordAttack swordAttack;
    public float movementSpeed;
    public float damping;
    public float runSpeed;
    public float runDamping;
    public bool inShop = false;
    public bool isAttacking = false;
    private float normalSpeed;
    private float normalDamping;
    public GameObject fire, hat;
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
        Attack();
    }

    void FixedUpdate()
    {
        if (!isAttacking)
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
        else
        {
            rigidbody.velocity = Vector2.zero;
        }

        if (rigidbody.velocity.x > 0)
        {
            playerRenderer.flipX = false;
            swordAttack.AttackRight();
        }
        else if (0 > rigidbody.velocity.x)
        {
            playerRenderer.flipX = true;
            swordAttack.AttackLeft();
        }

        if (rigidbody.velocity.y > 0)
        {
            lastMoveNegative = true;
            anim.SetBool("lastMoveNegative", lastMoveNegative);
        }
        else if (0 > rigidbody.velocity.y)
        {
            lastMoveNegative = false;
            anim.SetBool("lastMoveNegative", lastMoveNegative);
        }

        anim.SetFloat("speedX", rigidbody.velocity.x);
        anim.SetFloat("speedY", rigidbody.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            inShop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            inShop = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {

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
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            anim.SetTrigger("Attack");
            isAttacking = true;
           // swordAttack.Attack();

            StartCoroutine(EndAttack());
        }
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

}