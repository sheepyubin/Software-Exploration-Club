using UnityEngine;
using System.Collections;

public class RopeLauncher : MonoBehaviour
{
    public GameObject ropePrefab; // 로프 프리팹
    public float ropeSpeed = 5f; // 로프 늘어나는 속도
    public float maxRopeLength = 10f; // 로프 최대 길이

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 월드 좌표 변환
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // 로프 발사
            LaunchRope(mousePosition);
        }
    }

    void LaunchRope(Vector2 targetPosition)
    {
        // 플레이어 위치에서 발사
        Vector2 launchPosition = transform.position;
        // 발사 방향 계산
        Vector2 direction = (targetPosition - launchPosition).normalized;

        // 로프 생성
        GameObject newRope = Instantiate(ropePrefab, launchPosition, Quaternion.identity);
        // 로프 방향 설정
        newRope.transform.up = direction;
        // 로프 길이 설정
        float initialRopeLength = 0f;
        newRope.transform.localScale = new Vector3(0.5f, initialRopeLength, 1f);

        // 로프 스크립트 가져오기
        Rope ropeScript = newRope.GetComponent<Rope>();
        if (ropeScript != null)
        {
            // 로프 코드에 있는 ExtendRope 코루틴 호출
            StartCoroutine(ropeScript.ExtendRope(direction, launchPosition, ropeSpeed, maxRopeLength));
        }
        else
        {
            Debug.LogWarning("Rope component not found on the rope object.");
        }
    }
}
