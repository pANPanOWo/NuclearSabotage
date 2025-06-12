using UnityEngine;

public class Smg : Weapon
{

    // Start is called before the first frame update
    void Start()
    {
        damage = 20;
        cooldown = .2f;
        maxAmmo = 50;
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
