using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.UIElements;

public class Movement : MonoBehaviourPunCallbacks, IPunObservable
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 15f;
    public float ropeAcceleration = 10f;
    public float moveTowardsSpeed = 5f;
    public RopeLauncher ropeLauncher;
    public GhostEffect ghost;

    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashCool = 1;

    private Rigidbody2D rb;
    public bool isGrounded;

    private Animator ani;
    private SpriteRenderer sp;
    private bool isClimbing;

    public PlayerContainer container;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        container.AddisDead(photonView.OwnerActorNr, false);
    }

    void FixedUpdate()
    {
        if (photonView != null && photonView.IsMine && !container.ReturnisDead(photonView.OwnerActorNr))
        {
            if (isDashing)
            {
                return;
            }

            float moveInput = Input.GetAxis("Horizontal");

            if (moveInput != 0)
                ani.SetBool("isWalk", true);
            else
                ani.SetBool("isWalk", false);

            if (rb.velocity.x < 0)
            {
                ghost.isFlip = true;
                sp.flipX = true;
            }
            else
            {
                ghost.isFlip = false;
                sp.flipX = false;
            }

            Vector2 moveVelocity;

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

            if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled)
            {
                ani.SetBool("isClimbing", true);
                Climb();
            }
            else
            {
                isClimbing = false;
                ani.SetBool("isClimbing", false);
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

            // 위치 동기화
            photonView.RPC("SyncMovement", RpcTarget.Others, transform.position);
        }
    }

    void Update()
    {
        if (photonView != null && photonView.IsMine && !container.ReturnisDead(photonView.OwnerActorNr))
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetKey(KeyCode.LeftShift) && canDash)
            {
                ani.SetBool("isDashing", true);
                ghost.makeGhost = true;
                StartCoroutine(Dash());
            }
        }
    }

    void Climb()
    {
        Vector3 targetPosition = ropeLauncher.distanceJoint.connectedAnchor;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveTowardsSpeed * Time.deltaTime);
        transform.position = transform.position;
        isClimbing = true;
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
        ghost.makeGhost = false;
        ani.SetBool("isDashing", false);
        isDashing = false;

        yield return new WaitForSeconds(dashCool);

        canDash = true;
    }

    [PunRPC]
    void SyncJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    [PunRPC]
    void SyncDash()
    {
        StartCoroutine(Dash());
    }

    [PunRPC]
    void SyncRopeMove(Vector3 position)
    {
        transform.position = position;
    }

    [PunRPC]
    void SyncMovement(Vector3 newPosition)
    {
        // 다른 플레이어의 위치를 동기화
        transform.position = newPosition;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 사용하지 않음
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            transform.position = Vector3.zero;
        }
        if (other.CompareTag("Bullet"))
        {
            container.AddisDead(photonView.OwnerActorNr, true);
            sp.color = Color.black;
        }
    }
}