using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private int currentHealth;

    //[SerializeField] private Weapon weapon;

    private PlayerControllerKeyboard playerController;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = new PlayerControllerKeyboard();
        playerMovement = new PlayerMovement();
        playerMovement.Initialize(rb);

        currentHealth = maxHealth;
    }

    private void Update()
    {
        playerController.Update();

        playerMovement.Move(playerController.moveInput);
        if (playerController.isJumping)
            playerMovement.Jump();

       // if (playerController.isShooting && weapon != null)
         //   weapon.Shoot();
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
            Death();
    }

    public void GiveLife(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Death()
    {
        Debug.Log("Player muerto");
    }
}
