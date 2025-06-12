using UnityEngine;

public class Pistol : Weapon
{


    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        cooldown = .5f;

    }

    public override void OnInteract()
    {
        
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnBulletsEnd()
    {

    }
}
