using UnityEngine;

public class PlayerControllerKeyboard
{
    public Vector2 moveInput { get; private set; }
    public bool isJumping { get; private set; }
    public bool isShooting { get; private set; }

    public void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        isJumping = Input.GetButtonDown("Jump");
        isShooting = Input.GetButtonDown("Fire1");
    }
}
