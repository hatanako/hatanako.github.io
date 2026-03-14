using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("不需要管理")]
    public Rigidbody2D rb;
    public Animator anim;

    public InputManager input;
    private PlayerAnimController animCtrl;
    public PlayerAnimState stateType;

    #region 功能组件
    public PlayerMover move;
    public PlayerJumper jump;
    #endregion

    #region 状态机
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    public PlayerDashState dashState;
    public PlayerCrouchState crouchState;
    public PlayerSlideState slideState;
    public PlayerWallSlideState wallSlideState;
    #endregion

    private BaseState nowState;

    [Header("地面检测")]
    public float groundCheckDistance = 0.1f;
    public Transform groundCheck;
    private bool isGrounded = true;
    public bool IsGrounded => isGrounded;

    [Header("墙面检测")]
    public float wallCheckDistance = 0.1f;
    public Transform wallCheck;
    private bool isTouchingWall = false;
    public bool IsTouchingWall => isTouchingWall;
    public float faceDir = 1; // 1表示朝右，-1表示朝左

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        move = GetComponent<PlayerMover>();
        jump = GetComponent<PlayerJumper>();

        input = new InputManager();
        animCtrl = new PlayerAnimController();

        idleState = new PlayerIdleState();
        moveState = new PlayerMoveState();
        jumpState = new PlayerJumpState();
        fallState = new PlayerFallState();
        dashState = new PlayerDashState();
        crouchState = new PlayerCrouchState();
        slideState = new PlayerSlideState();
        wallSlideState = new PlayerWallSlideState();
    }

    private void Start()
    {
        nowState = idleState;
        stateType = PlayerAnimState.Idle;
    }

    private void Update()
    {
        CheckGround();
        faceDir = transform.localScale.x > 0 ? 1 : -1;
        CheckWall();
        nowState.Update(this, input);
        // 根据当前状态设置动画
        animCtrl.SetAnimState(stateType, anim);
    }

    public void ChangeState(BaseState newState)
    {
        nowState.Exit(this);
        nowState = newState;
        nowState.Enter(this);
    }

    /// <summary>
    /// 检测地面
    /// </summary>
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, 1 << LayerMask.NameToLayer("Ground"));
        isGrounded = hit.collider != null;
    }

    private void CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, Vector2.right * faceDir, wallCheckDistance, 1 << LayerMask.NameToLayer("Ground"));
        isTouchingWall = hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        // 仅在物体被选中时绘制
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * faceDir * wallCheckDistance);
    }

    public bool CanDash()
    {
        return input.IsDashPressed() && move.CanDash();
    }

}