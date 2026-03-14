using UnityEngine;

public class PlayerDashState : BaseState
{
    private float dashTime = 0.2f;
    public override void Enter(PlayerController ctrl)
    {
        ctrl.stateType = PlayerAnimState.Dash;
        ctrl.move.Dash(ctrl.transform);
        dashTime = ctrl.move.dashDuration;
    }

    public override void Exit(PlayerController ctrl)
    {
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        dashTime -= Time.deltaTime;
        ctrl.move.KeepDash();
        if (dashTime <= 0)
        {
            if (!ctrl.IsGrounded)
                ctrl.ChangeState(ctrl.fallState);
            else
                ctrl.ChangeState(ctrl.idleState);
        }
    }
}
