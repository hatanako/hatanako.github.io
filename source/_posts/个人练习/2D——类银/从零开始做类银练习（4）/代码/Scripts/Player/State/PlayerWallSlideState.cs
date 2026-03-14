using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {
        ctrl.stateType = PlayerAnimState.WallSlide;
    }

    public override void Exit(PlayerController ctrl)
    {
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        ctrl.jump.WallSlide();
        if (input.IsLeftRightPressed())
            return;
        if (ctrl.IsGrounded)
        {
            ctrl.ChangeState(ctrl.idleState);
        }
        else if(input.IsJumpPressed())
        {
            ctrl.jump.isWallJump = true;
            ctrl.ChangeState(ctrl.jumpState);
        }
         else if(!ctrl.IsTouchingWall || 
            (input.IsLeftMovePressed() && ctrl.faceDir == 1 || input.IsRightMovePressed() && ctrl.faceDir == -1))
        {
            ctrl.ChangeState(ctrl.fallState);
        }
    }
}
