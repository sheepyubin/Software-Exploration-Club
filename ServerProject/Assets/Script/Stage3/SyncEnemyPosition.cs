using UnityEngine;
using Photon.Pun;

public class SyncEnemyPosition : MonoBehaviourPun, IPunObservable
{
    private Vector3 syncPos;
    public EnemyMoveS3 enemyMove;

    private void Update()
    {
        // 로컬 플레이어인 경우에만 적의 위치를 업데이트합니다.
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * 10f);
        }
    }

    // 포톤 네트워크를 통해 적의 위치를 동기화합니다.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 로컬 플레이어인 경우에만 적의 위치를 전송합니다.
            if (photonView.IsMine)
            {
                stream.SendNext(transform.position);
            }
        }
        else
        {
            // 네트워크 플레이어인 경우에는 적의 위치를 수신합니다.
            syncPos = (Vector3)stream.ReceiveNext();
        }
    }
}
