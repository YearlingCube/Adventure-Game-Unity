using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    public int EnemyHealth = 3;

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
     
    float SwingTime = 1f;
    float SwingRate = 1f;
    public Sprite deadState;

    float HurtTime = 0f;
    float HurtRate = 0.8f;

    public int MaxDist = 10;
    public int MinDist = 1;

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
            if (!m_isDead)
            {
                if (Vector3.Distance(transform.position, Player.transform.position) <= MinDist)
                {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, m_speed * Time.deltaTime);
            m_animator.SetInteger("AnimState", 2);
                }
                if (Vector3.Distance(transform.position, Player.transform.position) >= MaxDist)
                {
                    m_animator.SetInteger("AnimState", 0);
                }
                Debug.Log(Vector3.Distance(transform.position, Player.transform.position) >= MaxDist);
            }
        }
            if (EnemyHealth <= 0)
            {
                if (!m_isDead)
                {
                    m_animator.SetTrigger("Death");
                    m_isDead = true;
                    this.enabled = false;
                    this.m_animator.enabled = false;
                    this.GetComponent<SpriteRenderer>().sprite = deadState;
                }
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
        //Death

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
        //    elses
        //        m_animator.SetInteger("AnimState", 0);
        //}
    }

    public void TakeDamage(int Damage)
    {
        if (Time.time > HurtTime)
        {
        EnemyHealth -= Damage;
        m_animator.SetTrigger("Hurt");
            HurtTime = Time.time + HurtRate;
        }


        Debug.Log("Hit Enemy");

    }
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Time.time > SwingTime)
            {
          m_animator.SetTrigger("Attack");
                SwingTime = Time.time + SwingRate;
            }
                

        }

    }



    private void OnDrawGizmos()
        {
            Gizmos.DrawLine(pos1.position, pos2.position);
        }
    }