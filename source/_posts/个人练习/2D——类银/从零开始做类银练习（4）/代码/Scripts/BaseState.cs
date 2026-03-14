public abstract class BaseState
{
    public abstract void Enter(PlayerController ctrl);
    public abstract void Update(PlayerController ctrl, InputManager input);
    public abstract void Exit(PlayerController ctrl);
}
