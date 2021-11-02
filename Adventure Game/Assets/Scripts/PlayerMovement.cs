using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float jumpForce;

    bool isGrounded = false;
    public Transform isGroundedCheck;
    public float checkGroundRadius;
    public LayerMask GroundLayer;

    public Transform Sword;
    public Animation SwordAttackAnim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SwordAttackAnim = GameObject.Find("Sword").GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
            Debug.Log(isGrounded);
        Move();
        Jump();
        GroundCheck();
        Attack();
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwordAttackAnim.Play();
        }
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveby = x * speed;
        rb.velocity = new Vector2(moveby, rb.velocity.y);
        if(x == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Sword.localRotation = Quaternion.Euler(0, 0, -30);
            Sword.transform.localPosition = new Vector3(0.05f, -0.0045f, 0f);
        } else if(x == -1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Sword.localRotation = Quaternion.Euler(0, 0, 30);
            Sword.transform.localPosition = new Vector3(-0.05f, -0.0045f, 0f);

        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }
    }
    void GroundCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, GroundLayer);
        float x = Input.GetAxisRaw("Horizontal");
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
}