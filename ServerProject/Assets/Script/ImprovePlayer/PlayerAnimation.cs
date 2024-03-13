using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimation : MonoBehaviourPunCallbacks
{
    public Movement movement; // 플레이어 이동 스크립트
    public RopeLauncher ropeLauncher; // 로프런처 스크립트
    private Animator ani; // 애니메이터 컴포넌트
    private Rigidbody2D rb; // 리지드바디 컴포넌트
    
    void Start()
    {
        ani = GetComponent<Animator>(); // 애니메이터 초기화
        rb = GetComponent<Rigidbody2D>(); // 리지드바디 초기화
    }

    void Update()
    {
        
        if (photonView != null && photonView.IsMine)
        {
            float moveInput = Input.GetAxis("Horizontal"); // 가로 입력값

            if (moveInput != 0) // 0 = 제자리, 0 != 이동중
                ani.SetBool("isWalk", true);
            else
                ani.SetBool("isWalk", false);

            if (Input.GetKey(KeyCode.Space) && ropeLauncher.distanceJoint.enabled) // 로프를 박은 상태로 스페이스를 눌렀을 때
                ani.SetBool("isClimbing", true);
            else
                ani.SetBool("isClimbing", false);

            if (movement.isGrounded)
                ani.SetBool("isJumping", false);

            if (!movement.isGrounded && !movement.isClimbing) // 땅에 닿아있지 않고 Climbing 상태가 아닐 때
            {
                if (rb.velocity.y > 0) // y 증가량이 > 0 이라면 
                    ani.SetBool("isJumping", true);

                else if (rb.velocity.y < 0) // y 증가량이  < 0 이라면
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
