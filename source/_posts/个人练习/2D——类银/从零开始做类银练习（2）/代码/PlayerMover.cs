using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 根据输入方向翻转角色朝向的方法，确保角色在移动时始终面向正确的方向。
    /// </summary>
    public void FlipAndMove(Transform transform, float dir)
    {
        if(dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
    }


}
