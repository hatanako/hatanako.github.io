using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机枚举
/// </summary>
public enum PlayerAnimState
{
    Idle = 0,
    Move = 1,
    Jump = 2,
    Fall = 3,
    Dash = 4,
    Crouch = 5,
    Slide = 6,
    WallSlide = 7,
    // 其他状态可以继续添加
}

/// <summary>
/// 动画控制器，负责根据状态机的状态切换动画
/// </summary>
public class PlayerAnimController
{
    private int nowState = 0;

    private PlayerAnimState state;

    public void SetAnimState(PlayerAnimState newState,Animator anim)
    {
        if (state == newState) return;
        state = newState;
        nowState = (int)state;
        anim.SetInteger("nowState", nowState);
    }
}
