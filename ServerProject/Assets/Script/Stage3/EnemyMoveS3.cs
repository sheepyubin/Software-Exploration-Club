using UnityEngine;

public class EnemyMoveS3 : MonoBehaviour
{
    public float minX = -5f; // X 축 최소 값
    public float maxX = 5f; // X 축 최대 값
    public float moveSpeed = 2f; // 이동 속도

    private int moveDirection = 1; // 이동 방향 (1: R, -1: L)

    private void Start()
    {
        // 이동 방향 설정
        SetRandomDirection();
    }

    private void Update()
    {
        // 현재 위치가 최소 또는 최대 X값에 도달하면 방향 전환
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            ChangeDirection();
        }

        // 이동
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * moveDirection);

        // 정해진 X 범위 내에서만 움직이도록 제한
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }

    // 무작위 방향 설정
    private void SetRandomDirection()
    {
        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    // 방향 전환
    private void ChangeDirection()
    {
        moveDirection *= -1; // 현재 방향을 반전시킴
    }

    // X 범위를 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, transform.position.y, transform.position.z), new Vector3(maxX - minX, 0.1f, 0.1f));
    }
}
