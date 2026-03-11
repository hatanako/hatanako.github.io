public class PlayerFallState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isFall", true);
    }
    public override void Exit(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isFall", false);
    }
    public override void Update(PlayerController ctrl, InputManager input)
    {
        if (ctrl.IsGrounded)
        {
            ctrl.ChangeState(ctrl.idleState);
        }
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
