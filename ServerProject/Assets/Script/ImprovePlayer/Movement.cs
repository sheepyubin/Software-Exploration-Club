using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Movement : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float jumpForce = 10f; // ����
    public float dashForce; // �뽬
    public RopeLauncher ropeLauncher; // RopeLauncher ��ũ��Ʈ

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canDash = true; // �뽬�� �����Ѱ�?
    private bool isDashing; // �뽬�ϴ� ���ΰ�?
    private float dashTime = 0.2f; // �뽬 ���� �ð�
    private float dashCool = 1; // �뽬 ��


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // rb �ʱ�ȭ
    }

    void Update()
    {
        if(isDashing)
        {
            return;
        }

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

        // �뽬
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("d");
            StartCoroutine(Dash());
        }
    }

    // �뽬
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float dashDirection = Mathf.Sign(rb.velocity.x); // �̵� ����
        rb.velocity = new Vector2(dashDirection * dashForce, 0); // �뽬

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCool);

        canDash = true;
    }
}
