using UnityEngine;

public class PlayerIdleState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {
        ctrl.rb.velocity =new Vector2(0,ctrl.rb.velocity.y);
    }

    public override void Exit(PlayerController ctrl)
    {

    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        if (input.IsLeftMovePressed() || input.IsRightMovePressed())
        {
            ctrl.ChangeState(ctrl.moveState);
        }
        else if (input.IsJumpPressed())
        {
            ctrl.ChangeState(ctrl.jumpState);
        }
        else if(ctrl.CanDash())
            ctrl.ChangeState(ctrl.dashState);
        else if (input.IsCrouchPressed())
            ctrl.ChangeState(ctrl.crouchState);
    }

}
