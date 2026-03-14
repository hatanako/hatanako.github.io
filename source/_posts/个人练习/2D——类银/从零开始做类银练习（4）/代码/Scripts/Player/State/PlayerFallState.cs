public class PlayerFallState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {
        ctrl.stateType = PlayerAnimState.Fall;
    }
    public override void Exit(PlayerController ctrl)
    {
    }
    public override void Update(PlayerController ctrl, InputManager input)
    {
        if (ctrl.IsGrounded)
        {
            ctrl.ChangeState(ctrl.idleState);
        }
        if(ctrl.CanDash())
        {
            ctrl.ChangeState(ctrl.dashState);
        }
        if (ctrl.IsTouchingWall && !ctrl.IsGrounded &&
            (
            (input.IsLeftMovePressed() && ctrl.faceDir == -1) ||
            (input.IsRightMovePressed() && ctrl.faceDir == 1))
            )
            ctrl.ChangeState(ctrl.wallSlideState);
        MoveOnAir(ctrl, input);
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
