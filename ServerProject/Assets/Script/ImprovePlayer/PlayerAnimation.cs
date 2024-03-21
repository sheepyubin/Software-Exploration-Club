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

    bool isFalling;
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
                    isFalling = true;
                    ani.SetBool("isJumping", false);
                }
                else
                {
                    ani.SetBool("isFalling", false);
                    isFalling = false;
                    ani.SetBool("isJumping", false);
                }
            }
            else
            {
                ani.SetBool("isFalling", false);
                isFalling = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && movement.canDash)
                ani.SetBool("isDashing", true);

            if (!movement.isDashing)
                ani.SetBool("isDashing", false);

            // �ִϸ��̼� ���¸� �ٸ� �÷��̾�� ����ȭ�մϴ�.
            photonView.RPC("SyncAnimationState", RpcTarget.Others, moveInput != 0, Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled, !movement.isGrounded, movement.isClimbing, isFalling, Input.GetKey(KeyCode.LeftShift) && movement.canDash, !movement.isDashing);
        }
    }

    [PunRPC]
    void SyncAnimationState(bool isWalking, bool isClimbing, bool isJumping, bool isFalling, bool isDashing, bool isIdle)
    {
        // �ٸ� �÷��̾��� �ִϸ��̼� ���¸� ����ȭ�մϴ�.
        ani.SetBool("isWalk", isWalking);
        ani.SetBool("isClimbing", isClimbing);
        ani.SetBool("isJumping", isJumping);
        ani.SetBool("isFalling", isFalling);
        ani.SetBool("isDashing", isDashing);
        ani.SetBool("isIdle", isIdle);
    }

    // ��Ʈ��ũ���� �����͸� ������ �� ȣ��˴ϴ�.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �� �޼���� �ִϸ��̼� ����ȭ�� ������ �����Ƿ� ������ �ʿ䰡 �����ϴ�.
    }
}
