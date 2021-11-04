using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    public GameObject GameManager;

    public bool isPaused = false;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;

    public GameObject Hearts;

    public Sprite deadState;
    
    public LayerMask EnemyLayer;
    
    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;

    public int PlayerHealth = 6;

    public float attackDelay = 0.8f;
    public float attackTime = 0f;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public Transform attackPoint;

    float HurtTime = 0f;
    float HurtRate = 0.8f;

    Transform currentPlatform;

    // Executes When Script is Started
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update() {

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0) // if is not left, but is turning left...
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // flip the opposite direction...
        }
        else if (inputX < 0) // if is not right, but is turning right then...
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // flip the opposite direction...
        }

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --

        //Death
        if (PlayerHealth <= 0)
        {
            if (!m_isDead)
            { 
                m_animator.SetTrigger("Death");
            m_isDead = true;
                this.m_animator.enabled = false;
                this.enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = deadState;
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GameManager.GetComponent<GameManager>().Die();
            }
        }

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > attackTime)
            {
                m_animator.SetTrigger("Attack");


                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayer);

                foreach (Collider2D enemy in hitEnemy)
                {

                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

                }
                attackTime = Time.time + attackDelay;

            }
            m_animator.SetInteger("AnimState", 1);
            m_combatIdle = true;

        }

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_animator.SetInteger("AnimState", 2);
            m_combatIdle = false;
        }
        else if (inputX == 0 && !m_combatIdle)
        {
            m_animator.SetInteger("AnimState", 0);

        }
        // Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
            GameManager.GetComponent<GameManager>().Pause();
            }
            else if (isPaused)
            {
                isPaused = false;
                GameManager.GetComponent<GameManager>().Resume();
            }
        }
    }
    // Take Damage
    public void TakeDamage(int Damage)
    {
        if (Time.time > HurtTime)
        {
            PlayerHealth -= Damage;
            m_animator.SetTrigger("Hurt");
            HurtTime = Time.time + HurtRate;
        }
    }
    // Gizmo OnDrawSelected
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moving Platform")
        {
            currentPlatform = coll.gameObject.transform;
            transform.SetParent(currentPlatform);
        }
        if (coll.gameObject.tag == "Hurt")
        {
            TakeDamage(1);
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moving Platform")
        {
            currentPlatform = null;
            transform.SetParent(currentPlatform);
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "WIN")
            {
                GameManager.GetComponent<GameManager>().NextLevel();
            }
           
        }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lever")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                collision.GetComponent<Lever>().FlipLever();
            }
        }
    }
}