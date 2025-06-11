using System.Collections;
using UnityEngine;

public class Kamikaze : Enemy
{
    [Header("Patrol settings")]
    private float patrolRadius = 5f;
    private float waitTime;

    [Header("Detection settings")]
    [SerializeField] private float detectionRange;

    private Rigidbody2D rbEnemy;
    private Transform playerTransform;
    private Vector2 patrolCenter;
    private Vector2 currentTarget;
    private bool waiting;

    private void Awake()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        waitTime = 1.5f;
    }

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        patrolCenter = transform.position;
        maxHealth = 50;
        currentHealth = maxHealth;
        moveSpeed = 3.5f;
        enemyState = EnemyState.Patrol;
        PickNewPatrolTarget();
    }

    private void Update()
    {
        CheckDistances();
        SwitchEnemyState();
    }

    private void CheckDistances()
    {
        if (playerTransform == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionRange && enemyState != EnemyState.Attack)
        {
            enemyState = EnemyState.Attack;
        }
    }

    private void SwitchEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                rbEnemy.velocity = Vector2.zero;
                break;
            case EnemyState.Patrol:
                if (!waiting)
                    EnemyMovement();
                break;
            case EnemyState.Attack:
                FollowPlayer();
                break;
        }
    }

    protected override void EnemyMovement()
    {
        Vector2 dir = (currentTarget - (Vector2)transform.position).normalized;
        rbEnemy.velocity = dir * moveSpeed;
        if (!waiting && Vector2.Distance(transform.position, currentTarget) < 0.2f)
        {
            waiting = true;
            StartCoroutine(WaitAndPickNewTarget());
        }
    }

    private IEnumerator WaitAndPickNewTarget()
    {
        waiting = true;
        rbEnemy.velocity = Vector2.zero;
        enemyState = EnemyState.Idle;
        yield return new WaitForSeconds(waitTime);
        PickNewPatrolTarget();
        enemyState = EnemyState.Patrol;
        waiting = false;
    }
    private void PickNewPatrolTarget()
    {
        float randomOffset = Random.Range(-patrolRadius, patrolRadius);
        currentTarget = new Vector2(patrolCenter.x + randomOffset, transform.position.y);
    }

    private void FollowPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rbEnemy.velocity = direction * moveSpeed;
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
}
