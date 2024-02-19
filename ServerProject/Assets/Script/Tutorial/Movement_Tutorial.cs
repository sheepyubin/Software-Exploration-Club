using UnityEngine;
using System.Collections;

public class Movement_Tutorial : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 15f;
    public float ropeAcceleration = 10f;
    public float moveTowardsSpeed = 5f;
    public RopeLauncher_Tutorial ropeLauncher;

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashCool = 1;

    // 튜토리얼
    private bool step1 = false;
    private bool step2 = false;
    public bool step3= false;
    private bool step5 = false;
    private int temp = 0;

    private bool isDead = false; // 죽었는지 안 죽었는지 판별

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        step1 = false;
        step2 = false;
        step3 = false;
        step5 = false ;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity;

        if (moveInput != 0)
        {
            step1 = true; // 튜토리얼 1단계 완료
        }

        if (ropeLauncher.distanceJoint.enabled)
        {
            moveVelocity = new Vector2(moveInput * moveSpeed * ropeAcceleration, rb.velocity.y);
        }
        else
        {
            moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        rb.velocity = moveVelocity;

        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.6f), 0.1f, LayerMask.GetMask("Ground"));

        if (Input.GetKey(KeyCode.LeftShift) && canDash && step1)
        {
            StartCoroutine(Dash());
            step2 = true; // 튜토리얼 2단계 완료
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && step2)
        {
            temp++;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if(temp >= 2)
            step3 = true; // 튜토리얼 3단계 완료
        }


        if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled && ropeLauncher.step4)
        {
            Vector3 targetPosition = ropeLauncher.distanceJoint.connectedAnchor;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveTowardsSpeed * Time.deltaTime);
            step5 = true;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float dashDirection = Mathf.Sign(rb.velocity.x);
        rb.velocity = new Vector2(dashDirection * dashForce, 0);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCool);

        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // EndPoint에 충돌
        if (collision.gameObject.CompareTag("EndPoint"))
        {
            if(step5)
            Debug.Log("Tutorial Clear");
        }

        // Boundary에 충돌
        if (collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("Bullet"))
        {
            isDead = true; // 플레이어 사망

            if(isDead)
            {
                // 여기에 관전 기능 구현
                // 카메라 자유시점
            }
        }
    }

    public void ResetUser()
    {
        transform.position =new Vector2 (-15, -1);

        step1 = false;
        step2 = false;
        step3 = false;
        step5 = false;
        temp = 0;
    }
}
