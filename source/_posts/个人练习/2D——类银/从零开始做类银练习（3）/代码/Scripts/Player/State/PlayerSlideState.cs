using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : BaseState
{
    private float slideTime = 0.1f;
    public override void Enter(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isSlide", true);
        slideTime = ctrl.move.slideDuration;
    }

    public override void Exit(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isSlide", false);
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        ctrl.move.Slide(ctrl.transform,input);
        slideTime -= Time.deltaTime;
        if (slideTime <= 0)
        {
            if(input.IsCrouchPressed())
                ctrl.ChangeState(ctrl.crouchState);
            else
                ctrl.ChangeState(ctrl.idleState);
        }
    }
}
