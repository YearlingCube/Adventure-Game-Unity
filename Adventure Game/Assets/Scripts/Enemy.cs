using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public EnemyHealthBar HealthBar;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    public Sprite deadState;

    public int EnemyHealth = 3;

    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;

    bool isAttacking = false;

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    public float attackRange = 0.5f;
    int attackDamage = 1;
    
    public Transform attackPoint;
    public LayerMask PlayerLayer;

    Vector3 nextPos;

    float SwingTime = 0f;
    public float SwingRate = 1f;

    float HurtTime = 0f;
    float HurtRate = 0.8f;
     
    public int MaxDist = 10;
    public int MinDist = 1;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        nextPos = Player.transform.position;
        HealthBar.SetMaxHealth(EnemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Flipping Enemy Based On Player's Position
            nextPos = Player.transform.position;

        if (nextPos.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
        }
        // Moving Enemy AI
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
        // Death
            if (EnemyHealth <= 0)
            {
                if (!m_isDead)
                {
                    m_animator.SetTrigger("Death");
                    m_isDead = true;
                    // Disabling Enemy
                    this.enabled = false;
                    this.m_animator.enabled = false;
                    this.GetComponent<SpriteRenderer>().sprite = deadState;
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
    }
    // Attack
    void Attack()
    {
        if (Time.time > SwingTime)
        {
            isAttacking = true;
            m_animator.SetTrigger("Attack");
            SwingTime = Time.time + SwingRate;

            Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, PlayerLayer);
            Debug.Log(HitPlayer);

            foreach (Collider2D Player in HitPlayer)
            {
                if (Player.GetComponent<Bandit>().Shielded == false)
                {
                Player.GetComponent<Bandit>().TakeDamage(attackDamage);
                }   
            }
        }
        else
        {
            m_animator.SetInteger("AnimState", 1);

        }


    }
    // Take Damage
    public void TakeDamage(int Damage)
    {
        if (Time.time > HurtTime)
        {
        EnemyHealth -= Damage;
            HealthBar.SetHealth(EnemyHealth);
        m_animator.SetTrigger("Hurt");
            HurtTime = Time.time + HurtRate;
        }
    }
    // If Enemy Touches Player Then Attack
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        Attack();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAttacking = false;
        }
    }
    // Gizmo's OnDrawSelected
    private void OnDrawGizmosSelected()
    {
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
        //