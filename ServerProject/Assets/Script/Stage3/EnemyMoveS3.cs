using UnityEngine;

public class EnemyMoveS3 : MonoBehaviour
{
    public float minX = -5f; // X �� �ּ� ��
    public float maxX = 5f; // X �� �ִ� ��
    public float moveSpeed = 2f; // �̵� �ӵ�

    private float minChangeInterval = 1f; // ���� ��ȯ �ּ� ����
    private float maxChangeInterval = 1.5f; // ���� ��ȯ �ִ� ����
    private float changeDirectionInterval; // ���� ��ȯ ����
    private int moveDirection = 1; // �̵� ���� (1: R, -1: L)

    private void Start()
    {
        // �̵� ���� ����
        SetRandomDirection();

        // ���� ��ȯ ���� �ʱ�ȭ
        SetRandomDirectionInterval();

        // ���� ��ȯ
        InvokeRepeating(nameof(SetRandomDirection), changeDirectionInterval, changeDirectionInterval);
    }

    private void Update()
    {
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

    // ������ ���� ��ȯ ���� ����
    private void SetRandomDirectionInterval()
    {
        changeDirectionInterval = Random.Range(minChangeInterval, maxChangeInterval);
    }

    // X ������ ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, transform.position.y, transform.position.z), new Vector3(maxX - minX, 0.1f, 0.1f));
    }
}
