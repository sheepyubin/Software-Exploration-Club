using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    Movement2D movement2D;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        movement2D = GetComponent<Movement2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        float x = Input.GetAxisRaw("Horizontal");
        movement2D.Move(x);
        
        Vector3 filpMove = Vector3.zero;
        if (x < 0)
        {
            filpMove = Vector3.left;
            transform.localScale = new Vector2(-movement2D.moveSize, movement2D.moveSize);
        }
        else if (x > 0)
        {
            filpMove = Vector3.right;
            transform.localScale = new Vector2(movement2D.moveSize, movement2D.moveSize);
        }

        // Walk Animation
        if (Mathf.Abs(movement2D.rigid2D.velocity.x) == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
        
        // �����̽��ٸ� ������ �����մϴ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                movement2D.rigid2D.AddForce(Vector2.up * movement2D.jumpPower, ForceMode2D.Impulse);
            }
        }

        var vel_norm = movement2D.rigid2D.velocity.normalized;
        anim.SetBool("isGround", vel_norm.y == 0);
        if (vel_norm.y != 0) anim.SetFloat("JumpBlend", movement2D.rigid2D.velocity.normalized.y);
    }
    // Ground üũ
    bool IsGrounded()
    {

        // Collider �� ����ũ�⸦ �̿��� ĳ������ ���ʿ� Box ������� �浹 üũ�� �մϴ�.
        Debug.DrawRay(movement2D.rigid2D.position, Vector3.down, new Color(0, 1, 0));
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
        movement2D.rigid2D.velocity = Vector2.zero;
    }
}