using UnityEngine;

public class PlayerMoveState : BaseState
{
    private float moveDir;
    public override void Enter(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isMove", true);
    }
    public override void Exit(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isMove", false);
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
    }

    private void Move(PlayerController ctrl, InputManager input)
    {
        moveDir = input.IsLeftMovePressed() ? -1 : (input.IsRightMovePressed() ? 1 : 0);
        ctrl.move.FlipAndMove(ctrl.transform, moveDir);
        // 根据实际速度（而非输入）更新动画，避免输入与移动不同步
        bool isMoving = Mathf.Abs(ctrl.rb.velocity.x) > 0.01f;
    }
}
