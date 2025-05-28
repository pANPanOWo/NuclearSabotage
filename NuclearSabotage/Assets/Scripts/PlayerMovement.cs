using UnityEngine;

public class PlayerMovement
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rb;

    public void Initialize(Rigidbody2D rb, float moveSpeed = 5f, float jumpForce = 7f)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
    }

    public void Move(Vector2 input)
    {
        rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
