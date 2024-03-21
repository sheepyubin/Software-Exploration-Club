using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimation : MonoBehaviourPunCallbacks, IPunObservable
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
            // �� �ڵ�� ���� �÷��̾�Ը� ����˴ϴ�.
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

    // ��Ʈ��ũ���� �����͸� ������ �� ȣ��˴ϴ�.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���� �÷��̾��� �ִϸ��̼� ���¸� ��Ʈ���� ����մϴ�.
            stream.SendNext(ani.GetBool("isWalk"));
            stream.SendNext(ani.GetBool("isClimbing"));
            stream.SendNext(ani.GetBool("isJumping"));
            stream.SendNext(ani.GetBool("isFalling"));
            stream.SendNext(ani.GetBool("isDashing"));
        }
        else
        {
            // ���� �÷��̾��� �ִϸ��̼� ���¸� ��Ʈ������ �о�ɴϴ�.
            ani.SetBool("isWalk", (bool)stream.ReceiveNext());
            ani.SetBool("isClimbing", (bool)stream.ReceiveNext());
            ani.SetBool("isJumping", (bool)stream.ReceiveNext());
            ani.SetBool("isFalling", (bool)stream.ReceiveNext());
            ani.SetBool("isDashing", (bool)stream.ReceiveNext());
        }
    }
}
