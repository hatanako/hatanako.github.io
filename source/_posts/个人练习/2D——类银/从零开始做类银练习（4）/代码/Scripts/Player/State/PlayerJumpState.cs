using UnityEngine;

public class PlayerJumpState : BaseState
{
    private float wallJumpTimer = 0.2f; // 蹬墙跳的持续时间
    public override void Enter(PlayerController ctrl)
    {
        if(!ctrl.jump.isWallJump)
            ctrl.jump.Jump(ctrl.IsGrounded);
        else
        {
            wallJumpTimer = 0.2f; // 重置蹬墙跳的持续时间
            ctrl.jump.WallJump(ctrl.faceDir);
        }
        ctrl.stateType = PlayerAnimState.Jump;
    }

    public override void Exit(PlayerController ctrl)
    {
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        wallJumpTimer -= Time.deltaTime;
        if (wallJumpTimer > 0)
            return;
        if (ctrl.rb.velocity.y <= 0)
        {
            ctrl.ChangeState(ctrl.fallState);
        }
        MoveOnAir(ctrl, input);
        if(ctrl.CanDash())
            ctrl.ChangeState(ctrl.dashState);
        if(ctrl.IsTouchingWall && !ctrl.IsGrounded && 
            (
            (input.IsLeftMovePressed() && ctrl.faceDir == -1) || 
            (input.IsRightMovePressed() && ctrl.faceDir == 1))
            )
            ctrl.ChangeState(ctrl.wallSlideState);
    }

    private static void MoveOnAir(PlayerController ctrl, InputManager input)
    {
        if (input.IsLeftMovePressed() || input.IsRightMovePressed())
        {
            float horizontal = input.IsLeftMovePressed() ? -1 : 1;
            ctrl.move.Move(ctrl.transform, horizontal);
        }
    }
}
