using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ÌøÔ¾·½·¨
    /// </summary>
    public void Jump(bool isGrounded)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
