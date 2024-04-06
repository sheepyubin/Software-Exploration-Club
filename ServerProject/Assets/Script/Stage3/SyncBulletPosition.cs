using UnityEngine;
using Photon.Pun;

public class SyncBulletPosition : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    private bool isBulletEnabled;

    private void Update()
    {
        if (!photonView.IsMine && !isBulletEnabled)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
            gameObject.SetActive(isBulletEnabled);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 현재 위치와 회전값, 활성 상태를 보낸다.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(gameObject.activeSelf); // 총알의 활성화 여부도 전송한다.
        }
        else
        {
            // 네트워크로부터 위치와 회전값, 활성 상태를 받는다.
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            isBulletEnabled = (bool)stream.ReceiveNext();
        }
    }
}
