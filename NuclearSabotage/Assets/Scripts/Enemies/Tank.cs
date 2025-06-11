using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    [Header("Tank Settings")]
    [SerializeField] private List<Transform> patrolPoints;
    private float waitTime;
    [SerializeField] private float detectionRange;

    private Rigidbody2D rb;
    private Transform player;
    private int currentPointIndex;
    private bool waiting;

    private void Awake()
    {
        waitTime = 2f;
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 200;
        currentHealth = maxHealth;
        moveSpeed = 1f;
        enemyState = EnemyState.Patrol;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;
        Checkdistances();
        SwitchEnemyStates();
    }

    private void Checkdistances()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && enemyState != EnemyState.Attack)
        {
            enemyState = EnemyState.Attack;
        }
    }

    private void SwitchEnemyStates()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                rb.velocity = Vector2.zero;
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
        if (patrolPoints.Count == 0 || waiting) return;
        Transform target = patrolPoints[currentPointIndex];
        float directionX = Mathf.Sign(target.position.x - transform.position.x);
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
        float distanceX = Mathf.Abs(target.position.x - transform.position.x);
        if (distanceX < 0.2f)
        {
            rb.velocity = Vector2.zero;
            waiting = true;
            StartCoroutine(WaitAndMoveNext());
        }
    }

    private IEnumerator WaitAndMoveNext()
    {
        enemyState = EnemyState.Idle;
        yield return new WaitForSeconds(waitTime);
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
        enemyState = EnemyState.Patrol;
        waiting = false;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
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
