using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float EnemyHealth = 6f;


    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    bool isAttacking = false;

    public Transform pos1;
    public Transform pos2;
    public Transform startpos;

    Vector3 nextPos;

    public GameObject Player;

    float AnimationTime = 1f;
    float SwingRate = 1f;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        nextPos = startpos.position;
    }

    // Update is called once per frame
    void Update()
    {
            nextPos = Player.transform.position;

        if (nextPos.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
        if (!isAttacking)
        {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, m_speed * Time.deltaTime);
            m_animator.SetInteger("AnimState", 2);
        }
        //Check if character just landed on the ground
        //    if (!m_grounded && m_groundSensor.State())
        //    {
        //        m_grounded = true;
        //        m_animator.SetBool("Grounded", m_grounded);
        //    }

        //    //Check if character just started falling
        //    if (m_grounded && !m_groundSensor.State())
        //    {
        //        m_grounded = false;
        //        m_animator.SetBool("Grounded", m_grounded);
        //    }




        //    //Set AirSpeed in animator
        //    m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        //    // -- Handle Animations --
        //    //Death
        //    if (EnemyHealth <= 0)
        //    {
        //        if (!m_isDead)
        //            m_animator.SetTrigger("Death");
        //        m_isDead = true;
        //        //else
        //        //    m_animator.SetTrigger("Recover");

        //        //m_isDead = !m_isDead;
        //    }

        //    //Attack
        //    else if (Input.GetMouseButtonDown(0))
        //    {
        //        m_animator.SetTrigger("Attack");
        //    }

        //    //Change between idle and combat idle
        //    else if (Input.GetKeyDown("f"))
        //        m_combatIdle = !m_combatIdle;

        //    //Jump
        //    else if (Input.GetKeyDown("space") && m_grounded)
        //    {
        //        m_animator.SetTrigger("Jump");
        //        m_grounded = false;
        //        m_animator.SetBool("Grounded", m_grounded);
        //        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        //        m_groundSensor.Disable(0.2f);
        //    }

        //    //Run
        //    else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        //        m_animator.SetInteger("AnimState", 2);

        //    //Combat Idle
        //    else if (m_combatIdle)
        //        m_animator.SetInteger("AnimState", 1);

        //    //Idle
        //    else
        //        m_animator.SetInteger("AnimState", 0);
        //}
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9)
            {
                EnemyHealth--;
                if (!m_isDead)
                {
                    m_animator.SetTrigger("Hurt");
                }

                Debug.Log(EnemyHealth);
            }
    }
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Time.time > AnimationTime)
            {
          m_animator.SetTrigger("Attack");
                AnimationTime = Time.time + SwingRate;
            }
                

        }

    }



    private void OnDrawGizmos()
        {
            Gizmos.DrawLine(pos1.position, pos2.position);
        }
    }