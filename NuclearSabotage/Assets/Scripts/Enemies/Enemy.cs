using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected int currentHealth;
    protected int maxHealth;
    //protected Weapon weapon;
    protected float moveSpeed;
    protected EnemyState enemyState;

    protected abstract void EnemyMovement();
    protected abstract void OnDeath();
    public virtual void GiveLife(int lifeAmount) { }
    public virtual void TakeDamage(int damage) { }
}
