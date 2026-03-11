using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isCrouch", true);
    }

    public override void Exit(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isCrouch", false);
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        ctrl.rb.velocity = new Vector2(0, ctrl.rb.velocity.y);
        if (!input.IsCrouchPressed())
        {
            ctrl.ChangeState(ctrl.idleState);
        }
        else if ((input.IsLeftMovePressed() || input.IsRightMovePressed())&&ctrl.move.CanSlide())
        {
            ctrl.ChangeState(ctrl.slideState);
        }
    }

}
