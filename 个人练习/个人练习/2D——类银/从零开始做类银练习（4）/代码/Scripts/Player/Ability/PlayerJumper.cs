using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    private Rigidbody2D rb;

    public float jumpForce = 10f;
    public float wallSlideSpeed = 2f;
    public float wallJumpHorizontalMultiplier = 8f;

    public bool isWallJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 跳跃方法
    /// </summary>
    public void Jump(bool isGrounded)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    /// <summary>
    /// 蹬墙跳方法
    /// </summary>
    /// <param name="faceDir">当前面朝向</param>
    public void WallJump(float faceDir)
    {
        rb.velocity = new Vector2(-faceDir * wallJumpHorizontalMultiplier, jumpForce);
        isWallJump = false;
    }

    /// <summary>
    /// 墙面滑行方法
    /// </summary>
    public void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }
}
