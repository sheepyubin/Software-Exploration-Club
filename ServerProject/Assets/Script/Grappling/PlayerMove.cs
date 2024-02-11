using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    // public GameManager gameManager;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    [SerializeField]
    public float speed = 5.0f; // 이동 속도
    public float jumpPower = 5.0f;
    public Rigidbody2D rigid2D;
    private PhotonView photonView; // PhotonView 변수 선언

    // Start is called before the first frame update
    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        //gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        photonView = GetComponent<PhotonView>(); // photonView 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView != null)
        {
            if (photonView.IsMine)
            {
            // Move
            float x = Input.GetAxisRaw("Horizontal");
            Move(x);
        
            Vector3 filpMove = Vector3.zero;
            if (x < 0)
            {
                filpMove = Vector3.left;
                spriteRenderer.flipX = true;
                //transform.localScale = new Vector2(-moveSize, moveSize);
            }
            else if (x > 0)
            {
                filpMove = Vector3.right;
                spriteRenderer.flipX = false;
                //transform.localScale = new Vector2(moveSize, moveSize);
            }

            // Walk Animation
            if (Mathf.Abs(rigid2D.velocity.x) == 0)
                anim.SetBool("isWalking", false);
            else
                anim.SetBool("isWalking", true);
        
            // 스페이스바를 누르면 점프합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    rigid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                }
            }

            var vel_norm = rigid2D.velocity.normalized;
            anim.SetBool("isGround", vel_norm.y == 0);
            if (vel_norm.y != 0) anim.SetFloat("JumpBlend", rigid2D.velocity.normalized.y);
            }
        }
        else
            Debug.LogWarning("photonView is NULL");
    }

    public void Move(float x)
    {
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }

    // Ground 체크
    bool IsGrounded()
    {

        // Collider 의 가로크기를 이용해 캐릭터의 발쪽에 Box 모양으로 충돌 체크를 합니다.
        Debug.DrawRay(rigid2D.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.BoxCast(
           capsuleCollider.bounds.center,
           new Vector2(capsuleCollider.bounds.size.x, capsuleCollider.bounds.size.y),
           0f,
           Vector2.down,
           0.01f,
           1 << LayerMask.NameToLayer("Ground"));
        //Debug.Log(rayHit);
        //Debug.Log(rayHit.collider);
        return rayHit.collider != null;
    }
    public void VelocityZero()
    {
        rigid2D.velocity = Vector2.zero;
    }
}
