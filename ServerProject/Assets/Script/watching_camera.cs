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
                EnableFreeCameraMode();
            }
        }

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
        freeCameraMode = true;
    }

    private void MoveFreeCamera(Vector3 moveDirection)
    {
        Debug.Log("MoveFreeCamera is called");
        transform.position += moveDirection * 7 * Time.deltaTime;
    }
}
