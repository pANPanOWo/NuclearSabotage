using System.Collections;
using UnityEngine;

public class Soldier : Enemy
{
    [Header("Component references")]
    private Rigidbody2D rbEnemy;
    private Transform playerTransform;

    [Header("Patrol references")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private float waitTime;
    [SerializeField] private float detectionRange;

    private Vector2 currentTarget;
    private bool waiting = false;
    private bool goingToB = true;

    private void Awake()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        waitTime = 2f;
    }

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        maxHealth = 100;
        currentHealth = maxHealth;
        moveSpeed = 2f;
        enemyState = EnemyState.Patrol;
        goingToB = true;
        currentTarget = pointB.position;
    }

    private void Update()
    {
        if (playerTransform == null) return;
        CheckDistances();
        SwitchEnemyState();
    }

    private void CheckDistances()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionRange && enemyState != EnemyState.Attack)
        {
            enemyState = EnemyState.Attack;
        }
        else if (distanceToPlayer > detectionRange && enemyState == EnemyState.Attack)
        {
            enemyState = EnemyState.Patrol;
            currentTarget = GetNextTarget();
            waiting = false;
        }
    }

    private void SwitchEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrol:
                if (!waiting)
                    EnemyMovement();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    protected override void EnemyMovement()
    {
        Vector2 direction = (currentTarget - (Vector2)transform.position).normalized;
        rbEnemy.velocity = direction * moveSpeed;

        float distance = Vector2.Distance(transform.position, currentTarget);
        if (distance < 0.5f)
        {
            StartCoroutine(WaitAndSwitchTarget());
        }
    }

    private IEnumerator WaitAndSwitchTarget()
    {
        waiting = true;
        rbEnemy.velocity = Vector2.zero;
        enemyState = EnemyState.Idle;

        yield return new WaitForSeconds(waitTime);

        currentTarget = GetNextTarget();
        enemyState = EnemyState.Patrol;
        waiting = false;
    }

    private Vector2 GetNextTarget()
    {
        goingToB = !goingToB; 
        return goingToB ? pointB.position : pointA.position;
    }

    private void Attack()
    {
        rbEnemy.velocity = Vector2.zero;
        Debug.Log("Soldier is attacking!");
    }

    protected override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemyState = EnemyState.Idle;
            OnDeath();
        }
    }

    public override void GiveLife(int lifeAmount)
    {
        currentHealth = Mathf.Min(currentHealth + lifeAmount, maxHealth);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
