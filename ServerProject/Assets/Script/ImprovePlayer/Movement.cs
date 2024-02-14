using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Movement : MonoBehaviourPunCallbacks, IPunObservable
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 15f;
    public float ropeAcceleration = 10f;
    public float moveTowardsSpeed = 5f;
    public RopeLauncher ropeLauncher;

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashCool = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
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

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                photonView.RPC("SyncJump", RpcTarget.Others);
            }

            if (Input.GetKey(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
                photonView.RPC("SyncDash", RpcTarget.Others);
            }

            if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled)
            {
                Vector3 targetPosition = ropeLauncher.distanceJoint.connectedAnchor;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveTowardsSpeed * Time.deltaTime);
                photonView.RPC("SyncRopeMove", RpcTarget.Others, transform.position);
            }

            // 좌우 이동 속도를 동기화
            photonView.RPC("SyncMoveVelocity", RpcTarget.Others, rb.velocity);
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
    void SyncMoveVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 사용하지 않음
    }
}