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

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashCool = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다. GameObject에 Rigidbody2D 컴포넌트가 추가되어 있는지 확인하세요.");
        }
    }

    void FixedUpdate()
    {
        if (photonView != null && photonView.IsMine)
        {
            if (isDashing)
            {
                return;
            }

            float moveInput = Input.GetAxis("Horizontal");
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

            isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -0.6f), 0.1f, LayerMask.GetMask("Ground"));

            if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled)
            {
                Vector3 targetPosition = ropeLauncher.distanceJoint.connectedAnchor;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveTowardsSpeed * Time.deltaTime);
                transform.position = transform.position;
            }

            // 위치 동기화
            photonView.RPC("SyncMovement", RpcTarget.Others, transform.position);
        }
    }

    void Update()
    {
        if (photonView != null && photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetKey(KeyCode.LeftShift) && canDash)
            {
                ghost.makeGhost = true;
                StartCoroutine(Dash());
            }

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
        ghost.makeGhost = false;
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
            Debug.Log("Dead");
        }
    }
}