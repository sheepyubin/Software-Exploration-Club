using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Movement : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프
    public float dashForce; // 대쉬
    public RopeLauncher ropeLauncher; // RopeLauncher 스크립트

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canDash = true; // 대쉬가 가능한가?
    private bool isDashing; // 대쉬하는 중인가?
    private float dashTime = 0.2f; // 대쉬 지속 시간
    private float dashCool = 1; // 대쉬 쿨


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rb 초기화
    }

    void Update()
    {
        if(isDashing)
        {
            return;
        }

        // 플레이어 이동
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // 땅에 닿아 있는가?
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.6f), 0.1f, LayerMask.GetMask("Ground"));

        // 플레이어 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 대쉬
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("d");
            StartCoroutine(Dash());
        }
    }

    // 대쉬
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float dashDirection = Mathf.Sign(rb.velocity.x); // 이동 방향
        rb.velocity = new Vector2(dashDirection * dashForce, 0); // 대쉬

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCool);

        canDash = true;
    }
}
