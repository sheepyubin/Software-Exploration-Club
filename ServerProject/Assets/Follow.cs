using UnityEngine;
using Photon.Pun;

public class Follow : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // 오브젝트 이동의 부드러운 정도
    public Vector3 offset; // 오브젝트와 플레이어 간의 거리
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
