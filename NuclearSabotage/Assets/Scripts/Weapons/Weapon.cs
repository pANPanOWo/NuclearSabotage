using UnityEngine;

public abstract class Weapon : MonoBehaviour, IInteractable
{
    protected int damage;
    protected float cooldown;
    protected int currentAmmo;
    protected int maxAmmo;

    public abstract void Shoot();
    public virtual void OnInteract() { }
    protected virtual void OnBulletsEnd() { }

}
