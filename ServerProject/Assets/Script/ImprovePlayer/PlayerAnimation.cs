using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimation : MonoBehaviourPunCallbacks
{
    public Movement movement; // �÷��̾� �̵� ��ũ��Ʈ
    public RopeLauncher ropeLauncher; // ������ó ��ũ��Ʈ
    private Animator ani; // �ִϸ����� ������Ʈ
    private Rigidbody2D rb; // ������ٵ� ������Ʈ
    
    void Start()
    {
        ani = GetComponent<Animator>(); // �ִϸ����� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>(); // ������ٵ� �ʱ�ȭ
    }

    void Update()
    {
        
        if (photonView != null && photonView.IsMine)
        {
            float moveInput = Input.GetAxis("Horizontal"); // ���� �Է°�

            if (moveInput != 0) // 0 = ���ڸ�, 0 != �̵���
                ani.SetBool("isWalk", true);
            else
                ani.SetBool("isWalk", false);

            if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled) // ������ ���� ���·� �����̽��� ������ ��
                ani.SetBool("isClimbing", true);
            else
                ani.SetBool("isClimbing", false);

            if (movement.isGrounded)
                ani.SetBool("isJumping", false);

            if (!movement.isGrounded && !movement.isClimbing) // ���� ������� �ʰ� Climbing ���°� �ƴ� ��
            {
                if (rb.velocity.y > 0) // y �������� > 0 �̶�� 
                    ani.SetBool("isJumping", true);

                else if (rb.velocity.y < 0) // y ��������  < 0 �̶��
                {
                    ani.SetBool("isFalling", true);
                    ani.SetBool("isJumping", false);
                }
                else
                {
                    ani.SetBool("isFalling", false);
                    ani.SetBool("isJumping", false);
                }
            }
            else
                ani.SetBool("isFalling", false);

            if (Input.GetKey(KeyCode.LeftShift) && movement.canDash)
                ani.SetBool("isDashing", true);

            if (!movement.isDashing)
                ani.SetBool("isDashing", false);
        }
    }
}
