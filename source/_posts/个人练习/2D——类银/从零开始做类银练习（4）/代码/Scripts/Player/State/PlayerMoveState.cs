using UnityEngine;

public class PlayerMoveState : BaseState
{
    private float moveDir;
    public override void Enter(PlayerController ctrl)
    {
        ctrl.stateType = PlayerAnimState.Move;
    }
    public override void Exit(PlayerController ctrl)
    {
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        Move(ctrl, input);

        if (moveDir == 0)
        {
            ctrl.ChangeState(ctrl.idleState);
        }
        else if (input.IsJumpPressed())
        {
            ctrl.ChangeState(ctrl.jumpState);
        }
        else if(ctrl.CanDash())
                ctrl.ChangeState(ctrl.dashState);
        else if(ctrl.move.CanSlide() && input.IsCrouchPressed())
                ctrl.ChangeState(ctrl.slideState);
    }

    private void Move(PlayerController ctrl, InputManager input)
    {
        moveDir = input.IsLeftMovePressed() ? -1 : (input.IsRightMovePressed() ? 1 : 0);
        ctrl.move.Move(ctrl.transform, moveDir);
        // 根据实际速度（而非输入）更新动画，避免输入与移动不同步
        bool isMoving = Mathf.Abs(ctrl.rb.velocity.x) > 0.01f;
    }
}
