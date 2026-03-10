using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("不需要管理")]
    public Rigidbody2D rb;
    public Animator anim;

    public InputManager input;

    #region 功能组件
    public PlayerMover move;
    public PlayerJumper jump;
    #endregion

    #region 状态机
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    #endregion

    private BaseState nowState;

    [Header("地面检测")]
    public float groundCheckDistance = 0.1f;
    public Transform groundCheck;
    private bool isGrounded = true;
    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        move = GetComponent<PlayerMover>();
        jump = GetComponent<PlayerJumper>();

        input = new InputManager();

        idleState = new PlayerIdleState();
        moveState = new PlayerMoveState();
        jumpState = new PlayerJumpState();
        fallState = new PlayerFallState();
    }

    private void Start()
    {
        nowState = idleState;
    }

    private void Update()
    {
        CheckGround();
        nowState.Update(this, input);
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
        anim.SetBool("isGround", isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        // 仅在物体被选中时绘制
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }

}