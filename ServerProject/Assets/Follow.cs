using UnityEngine;
using Photon.Pun;

public class Follow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // ������Ʈ �̵��� �ε巯�� ����
    public Vector3 offset; // ������Ʈ�� �÷��̾� ���� �Ÿ�
    public bool isDead = false;

    void Update()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null && !isDead)
        {
            GameObject localPlayer = GameObject.FindWithTag("Player");
            if (localPlayer != null)
            {
                Vector3 desiredPosition = localPlayer.transform.position + offset;
                desiredPosition.z = -1;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}
