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

    // Ʃ�丮��
    public bool step1 = false;
    public bool step2 = false;
    public bool step3= false;
    public bool step5 = false;
    private int temp = 0;

    private Animator ani;
    private SpriteRenderer sp;
    private bool isClimbing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        AudioManager.instance.PlayBgm(true);
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
            step1 = true; // Ʃ�丮�� 1�ܰ� �Ϸ�
            ani.SetBool("isWalk", true);
        }
        else
            ani.SetBool("isWalk", false);

        if (rb.velocity.x < 0)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
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

        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.8f), 0.1f, LayerMask.GetMask("Ground"));

        if (Input.GetKey(KeyCode.LeftShift) && canDash && step1)
        {
            ani.SetBool("isDashing", true);

            StartCoroutine(Dash());
            step2 = true; // Ʃ�丮�� 2�ܰ� �Ϸ�
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && step2)
        {
            temp++;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if(temp >= 2)
            step3 = true; // Ʃ�丮�� 3�ܰ� �Ϸ�
        }


        if (ropeLauncher.step4)
        {
            if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled)
            {
                ani.SetBool("isClimbing", true);
                Vector3 targetPosition = ropeLauncher.distanceJoint.connectedAnchor;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveTowardsSpeed * Time.deltaTime);
                step5 = true;
            }
            else
            {
                isClimbing = false;
                ani.SetBool("isClimbing", false);
            }
        }


        if (!isGrounded && !isClimbing)
        {
            if (rb.velocity.y > 0)
                ani.SetBool("isJumping", true);

            else if (rb.velocity.y < 0)
            {
                ani.SetBool("isFalling", true);
                ani.SetBool("isJumping", false);
            }
        }
        else
            ani.SetBool("isFalling", false);
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
        ani.SetBool("isDashing", false);
        isDashing = false;

        yield return new WaitForSeconds(dashCool);

        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // EndPoint�� �浹
        if (collision.gameObject.CompareTag("EndPoint"))
        {
            if(step5)
            Debug.Log("Tutorial Clear");
        }

        // Boundary�� �浹
        if (collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("Bullet"))
        {
            transform.position = new Vector2(-15, -2);
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
