using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage);
    public void GiveLife(int lifeAmount);
}
