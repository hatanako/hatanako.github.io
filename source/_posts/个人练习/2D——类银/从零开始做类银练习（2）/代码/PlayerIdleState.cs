public class PlayerIdleState : BaseState
{
    public override void Enter(PlayerController ctrl)
    {

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
    }

}
