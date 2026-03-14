using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : BaseState
{
    private float slideTime = 0.1f;
    public override void Enter(PlayerController ctrl)
    {
        ctrl.stateType = PlayerAnimState.Slide;
        ctrl.move.Slide(ctrl.transform, ctrl.input);
        slideTime = ctrl.move.slideDuration;
    }

    public override void Exit(PlayerController ctrl)
    {
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        slideTime -= Time.deltaTime;
        ctrl.move.KeepSlide();
        if (slideTime <= 0)
        {
            if(input.IsCrouchPressed())
                ctrl.ChangeState(ctrl.crouchState);
            else
                ctrl.ChangeState(ctrl.idleState);
        }
    }
}
