using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float lookRadius = 15f, attackRadius = 1.7f;
    private float attackTimer = 0.5f;
    private float attackCooldown;
    public int health = 100;
    public GameObject enemyLight;
    public GameObject enemyHand;
    public GameObject bloodGrenade;
    private bool attacked;
    public GameObject deathUI;
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    Collider enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        enemyCollider = GetComponent<Collider>();
        attackCooldown = attackTimer;
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (!deathUI.activeSelf)
        {
            if (distance <= lookRadius && health > 0)
            {
                agent.SetDestination(target.position);
                animator.SetBool("isFollowing", true);
            }
            if (attacked == false && distance <= attackRadius)
            {
                animator.SetBool("isAttacking", true);
                attacked = true;
            }
        }
        if (attackCooldown > 0 && attacked == true)
        {
            attackCooldown -= Time.deltaTime;
        }
        else if (attackCooldown <= 0 && attacked == true)
        {
            attacked = false;
            attackCooldown = attackTimer;
            animator.SetBool("isAttacking", false);
        }

        if (deathUI.activeSelf)
        {
            attacked = true;
            agent.isStopped = true;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isFollowing", false);
            animator.SetBool("playerDead", true);
        }
        if (health <= 0)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            animator.SetBool("isDead", true);
            enemyLight.SetActive(false);
            enemyCollider.enabled = false;
            enemyHand.GetComponent<Collider>().enabled = false;
        }
    }
    // Draws wire sphere around enemy to visualise the radius of look
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
    }
    public void SpillBlood()
    {
        float time = 2.0f;
        float timer = time;
        Instantiate(bloodGrenade, transform.position, transform.rotation);
    }
}
