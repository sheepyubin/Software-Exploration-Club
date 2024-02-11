using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{
    private bool isColled = false;

    public IEnumerator ExtendRope(Vector2 direction, Vector2 launchPosition, float ropeSpeed, float maxRopeLength)
    {
        float currentRopeLength = 0f;
        float elapsedTime = 0f;

        while (currentRopeLength < maxRopeLength)
        {
            // 충돌 감지
            if (isColled)
            {
                break; // 충돌이 감지되면 while 루프 종료
            }

            // 로프 늘리기
            elapsedTime += Time.deltaTime;
            currentRopeLength = Mathf.Lerp(0f, maxRopeLength, elapsedTime * ropeSpeed);

            // 로프 스케일 조정
            transform.localScale = new Vector3(0.5f, currentRopeLength, 1f);

            // 로프의 끝 부분 위치 조정
            Vector2 endPoint = launchPosition + direction * currentRopeLength * 0.5f;
            transform.position = endPoint;

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ground 태그에 닿았을 때
        if (other.CompareTag("Ground"))
        {
            isColled = true;
        }
    }
}
