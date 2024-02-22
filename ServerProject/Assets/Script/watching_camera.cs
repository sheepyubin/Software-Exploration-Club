using System.Collections;
using UnityEngine;

public class watching_camera : MonoBehaviour
{
    public Transform player;
    public float smoothing = 0.2f;
    public Vector2 minCameraBoundary;
    public Vector2 maxCameraBoundary;

    private bool playerDead = false;
    private bool freeCameraMode = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!playerDead)
        {
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

            // 5초 뒤에 플레이어 사망 판정
            StartCoroutine(WaitAndSetPlayerDead(5f));
        }
        else
        {
            // 플레이어가 사망한 경우
            if (!freeCameraMode)
            {
                // 플레이어 사망처리 로직을 추가
                // 예를 들어, 플레이어 사망 애니메이션 재생 등

                // 카메라를 wasd로 움직일 수 있는 자유 시점으로 전환
                EnableFreeCameraMode();
            }
        }

        // 자유 시점 카메라 모드에서의 움직임 로직을 추가
        // 예를 들어, Input.GetAxis("Horizontal") 및 Input.GetAxis("Vertical")로 이동 구현
        if (freeCameraMode)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            MoveFreeCamera(moveDirection);
        }
    }

    IEnumerator WaitAndSetPlayerDead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerDead = true;
    }

    private void EnableFreeCameraMode()
    {
        // wasd로 카메라를 자유롭게 움직일 수 있는 로직을 추가
        // 여기서는 간단하게 freeCameraMode를 true로 설정
        freeCameraMode = true;
    }

    private void MoveFreeCamera(Vector3 moveDirection)
    {
        Debug.Log("MoveFreeCamera is called");

        // wasd로 이동하는 로직을 추가
        // 여기서는 간단하게 카메라를 이동시킵니다.
        transform.position += moveDirection * smoothing*60 * Time.deltaTime;
    }
}
