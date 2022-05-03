using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int state;
    public NavMeshAgent agent;

    bool ishurt;

    Transform player;
    public Transform lips;
    Rigidbody rigidB;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject bullet, damageText, bloodSplat;


    public int currentHealth,maxHealth;
    //public Healthbar healthbar;
    public Animator anim;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile, slimespit;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    public void PlayerCombatDead()
    {
        player.gameObject.GetComponent<Combat>().Deadagainlol();
    }



    private void Awake()
    {
        player = GameObject.Find("player").transform;
        rigidB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        //currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            currentHealth -= damage;
            DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage);
            GameObject bloodyblood = Instantiate(bloodSplat, transform.position, Quaternion.identity);
            Destroy(bloodyblood, 2);
            anim.Play("hurt");
        }

        if (currentHealth <= 0) anim.Play("dead");
    }
    private void Update()
    {

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            //agent.enabled = false;
            rigidB.isKinematic = false;
            agent.updatePosition = false;
            rigidB.AddForce(transform.forward * -52f * Time.deltaTime, ForceMode.Impulse);
            rigidB.AddForce(transform.up * 32f * Time.deltaTime, ForceMode.Impulse);
            //agent.velocity = rigidB.velocity;
            ishurt = true;
            
        }
        else
        {
            agent.updatePosition = true;
            ishurt = false;
            //agent.enabled = true;
            rigidB.isKinematic = true;
        }



        if(rigidB.velocity.magnitude <= 0.1)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);

        }



        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            anim.SetFloat("speed", 1);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    void FixedUpdate()
    {
        //healthbar.SetHealth(currentHealth);

    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        //if (ishurt == false)
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
      


        transform.LookAt(player);

        if (!alreadyAttacked && !anim.GetCurrentAnimatorStateInfo(0).IsName("hurt") && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            //Make sure enemy doesn't move
            //if (ishurt == false)
            agent.SetDestination(transform.position);

            anim.Play("spit");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
         
    }


    public void ShootSlime()
    {
        ///Attack code here
        Rigidbody rb = Instantiate(projectile, lips.position, Quaternion.identity).GetComponent<Rigidbody>();
        GameObject slimeSpit = Instantiate(slimespit, lips.position, Quaternion.identity);
        Destroy(slimeSpit, 2);
        Destroy(rb.gameObject, 2f);
        rb.AddForce(transform.forward * 64f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///End of attack code
    }



    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void DestroyEnemy()
    {

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
