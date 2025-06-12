using UnityEngine;

public class Shotgun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 35;
        cooldown = .5f;
        maxAmmo = 5;
        currentAmmo = maxAmmo;
    }

    public override void OnInteract()
    {
        
    }

    protected override void OnBulletsEnd()
    {
        
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

}
