using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Weapon currentWeapon;

    void Start()
    {
        // Iniciás con una pistola
        currentWeapon = new Pistol();
    }

    void Update()
    {
        // Disparo con clic izquierdo
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentWeapon.Shoot();
        }

        // Cambio a pistola si se queda sin balas (con tecla R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeapon.currentAmmunition <= 0)
            {
                Debug.Log("Cambiando a pistola...");
                currentWeapon = new Pistol();
            }
        }

        // Podés cambiar de arma con teclas numéricas
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentWeapon = new Pistol();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentWeapon = new Shotgun();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentWeapon = new Smg();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            currentWeapon = new Fist();
    }
}

// base de armas
public abstract class Weapon : IInteractable
{
    public int damage;
    public float cooldown;
    public int currentAmmunition;
    public int maxAmmunition;
    public string onMsgString;

    string IInteractable.onMsgString { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public abstract void Shoot();

    public virtual void OnInteract()
    {
        // Por defecto no hace nada, se puede sobreescribir
    }

    public virtual void OnBulletsEnd()
    {
        // Acción cuando se queda sin balas
    }
}

// Arma: Pistola (munición infinita)
public class Pistol : Weapon
{
    public Pistol()
    {
        damage = 10;
        cooldown = 0.3f;
        maxAmmunition = int.MaxValue;
        currentAmmunition = maxAmmunition;
    }

    public override void Shoot()
    {
        // Lógica de disparo
        Debug.Log("Disparo con pistola. Daño: " + damage);
    }
}

// Arma: Escopeta
public class Shotgun : Weapon
{
    public Shotgun()
    {
        damage = 40;
        cooldown = 1.5f;
        maxAmmunition = 6;
        currentAmmunition = maxAmmunition;
    }

    public override void Shoot()
    {
        if (currentAmmunition <= 0)
        {
            OnBulletsEnd();
            return;
        }
        Debug.Log("Disparo con escopeta. Daño: " + damage);
        currentAmmunition--;
    }

    public override void OnBulletsEnd()
    {
        Debug.Log("Escopeta sin munición. Se descarta.");
        // Aquí se debería cambiar al arma base (pistola)
    }
}

// Arma: SMG
public class Smg : Weapon
{
    public Smg()
    {
        damage = 15;
        cooldown = 0.1f;
        maxAmmunition = 30;
        currentAmmunition = maxAmmunition;
    }

    public override void Shoot()
    {
        if (currentAmmunition <= 0)
        {
            OnBulletsEnd();
            return;
        }
        Debug.Log("Disparo con SMG. Daño: " + damage);
        currentAmmunition--;
    }

    public override void OnBulletsEnd()
    {
        Debug.Log("SMG sin munición. Se descarta.");
        // Aquí también se cambia a la pistola
    }
}

// Arma: Puño (golpe cuerpo a cuerpo)
public class Fist : Weapon
{
    public Fist()
    {
        damage = 5;
        cooldown = 0.4f;
        maxAmmunition = int.MaxValue; // no necesita munición
        currentAmmunition = maxAmmunition;
    }

    public override void Shoot()
    {
        Debug.Log("Golpe con puño. Daño: " + damage);
        // Aquí podrías lanzar una animación y detectar colisión cercana
    }

    public override void OnInteract()
    {
        Debug.Log("Golpea un objeto cercano.");
    }

    public override void OnBulletsEnd()
    {
        // Nada, ya que no usa balas
    }
}
