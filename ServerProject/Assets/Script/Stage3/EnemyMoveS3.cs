using UnityEngine;

public class EnemyMoveS3 : MonoBehaviour
{
    public float minX = -5f; // X �� �ּ� ��
    public float maxX = 5f; // X �� �ִ� ��
    public float moveSpeed = 2f; // �̵� �ӵ�

    private int moveDirection = 1; // �̵� ���� (1: R, -1: L)

    private void Start()
    {
        // �̵� ���� ����
        SetRandomDirection();
    }

    private void Update()
    {
        // ���� ��ġ�� �ּ� �Ǵ� �ִ� X���� �����ϸ� ���� ��ȯ
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            ChangeDirection();
        }

        // �̵�
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * moveDirection);

        // ������ X ���� �������� �����̵��� ����
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }

    // ������ ���� ����
    private void SetRandomDirection()
    {
        moveDirection = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    // ���� ��ȯ
    private void ChangeDirection()
    {
        moveDirection *= -1; // ���� ������ ������Ŵ
    }

    // X ������ ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, transform.position.y, transform.position.z), new Vector3(maxX - minX, 0.1f, 0.1f));
    }
}
