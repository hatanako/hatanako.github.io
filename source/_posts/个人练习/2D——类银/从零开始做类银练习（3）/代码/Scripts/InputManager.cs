using UnityEngine;

public class InputManager
{

    public KeyCode leftMove = KeyCode.LeftArrow;
    public KeyCode rightMove = KeyCode.RightArrow;
    public KeyCode jump = KeyCode.C;
    public KeyCode dash = KeyCode.LeftShift;
    public KeyCode attack = KeyCode.Z;
    public KeyCode crouch = KeyCode.DownArrow;

    public bool IsLeftMovePressed() => Input.GetKey(leftMove);
    public bool IsRightMovePressed() => Input.GetKey(rightMove);
    public bool IsJumpPressed() => Input.GetKeyDown(jump);
    public bool IsDashPressed() => Input.GetKeyDown(dash);
    public bool IsAttackPressed() => Input.GetKeyDown(attack);
    public bool IsCrouchPressed() => Input.GetKey(crouch);

}
