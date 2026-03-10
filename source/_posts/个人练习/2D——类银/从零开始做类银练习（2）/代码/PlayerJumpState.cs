public class PlayerJumpState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {
        ctrl.jump.Jump(ctrl.IsGrounded);
        ctrl.anim.SetBool("isJump", true);
    }

    public override void Exit(PlayerController ctrl)
    {
        ctrl.anim.SetBool("isJump", false);
    }

    public override void Update(PlayerController ctrl, InputManager input)
    {
        if (ctrl.rb.velocity.y <= 0)
        {
            ctrl.ChangeState(ctrl.fallState);
        }
        MoveOnAir(ctrl, input);
    }

    private static void MoveOnAir(PlayerController ctrl, InputManager input)
    {
        if (input.IsLeftMovePressed() || input.IsRightMovePressed())
        {
            float horizontal = input.IsLeftMovePressed() ? -1 : 1;
            ctrl.move.FlipAndMove(ctrl.transform, horizontal);
        }
    }
}
