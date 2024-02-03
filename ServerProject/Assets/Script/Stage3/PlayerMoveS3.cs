using UnityEngine;
using Photon.Pun;

public class PlayerMoveS3 : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float jumpForce = 10f; // ���� ��

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rb �ʱ�ȭ
    }

    void Update()
    {
        // �÷��̾� �̵�
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // ���� ��� �ִ°�?
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.6f), 0.1f, LayerMask.GetMask("Ground"));

        // �÷��̾� ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
