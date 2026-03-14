using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 8f;
    public float dashSpeed = 12f;
    public float slideSpeed = 15f;

    public float dashDuration = 0.3f;
    public float dashCooldown = 1f;
    public float dashTimer = 0f;

    public float slideDuration = 0.1f;
    public float slideCooldown = 1f;
    public float slideTimer = 0f;

    public float GCD = 0.3f; // 全局冷却时间，防止冲刺和滑行同时触发
    private float GCDTimer = 0.3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        dashTimer += Time.deltaTime;
        slideTimer += Time.deltaTime;
        GCDTimer -= Time.deltaTime;
    }

    /// <summary>
    /// 移动方法
    /// </summary>
    public void Move(Transform transform, float dir)
    {
        Flip(transform, dir);
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
    }

    /// <summary>
    /// 翻转角色朝向的方法，根据输入的方向参数调整角色的本地缩放，以实现角色在水平轴上的翻转效果。
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="dir"></param>
    private static void Flip(Transform transform, float dir)
    {
        if (dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// 冲刺方法，允许角色在短时间内以更高的速度移动。通过调整角色的速度和持续时间来实现冲刺效果，同时确保在冲刺结束后恢复正常速度。
    /// </summary>
    /// <param name="transform"></param>
    public void Dash(Transform transform)
    {
        dashTimer = 0f;
        GCDTimer = GCD; // 重置全局冷却时间
        float dir = transform.localScale.x; // 获取当前角色的朝向
        rb.velocity = new Vector2(dir * dashSpeed, 0);

    }

    public void KeepDash()
    {
        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);
    }

    /// <summary>
    /// 判断角色是否可以进行冲刺的方法，基于冲刺冷却时间和当前计时器的状态来确定角色是否能够执行冲刺动作。
    /// </summary>
    /// <returns></returns>
    public bool CanDash()
    {
        return ((dashTimer >= dashCooldown) && (GCDTimer <= 0));
    }

    public void Slide(Transform transform, InputManager input)
    {
        slideTimer = 0f;
        GCDTimer = GCD; // 重置全局冷却时间
        float faceDir = transform.localScale.x; // 获取当前角色的朝向
        float dir = input.IsLeftMovePressed() ? -1 : (input.IsRightMovePressed() ? 1 : faceDir); // 获取当前角色的朝向
        Flip(transform, dir);
        rb.velocity = new Vector2(dir * slideSpeed, 0);
    }

    public void KeepSlide()
    {
        rb.velocity = new Vector2(slideSpeed * transform.localScale.x, rb.velocity.y);
    }

    public bool CanSlide()
    {
        return ((slideTimer >= slideCooldown) && (GCDTimer <= 0));
    }


}
