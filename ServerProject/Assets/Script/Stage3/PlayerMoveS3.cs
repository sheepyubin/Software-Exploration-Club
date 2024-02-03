using UnityEngine;
using Photon.Pun;

public class PlayerMoveS3 : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rb 초기화
    }

    void Update()
    {
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
    }
}
