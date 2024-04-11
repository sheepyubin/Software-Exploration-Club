using UnityEngine;
using Photon.Pun;

public class Missile : MonoBehaviourPun, IPunObservable
{
    public float speed = 15f; // 미사일 속도
    public float destroyX = -20f; // 파괴할 X 좌표
    public GameObject explosionPrefab; // 폭발 이펙트 프리팹

    void Update()
    {
        // 미사일을 왼쪽으로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // 일정 X 좌표 이상이 되면 파괴
        if (transform.position.x <= destroyX)
        {
            DestroyMissile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 객체가 플레이어일 경우
        if (other.CompareTag("Player"))
        {
            // 폭발 이펙트 생성
            PhotonNetwork.Instantiate(explosionPrefab.name, transform.position, Quaternion.identity);
            // 미사일 파괴
            DestroyMissile();
        }
    }

    void DestroyMissile()
    {
        // 파괴 RPC 호출
        photonView.RPC("RPC_DestroyMissile", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RPC_DestroyMissile()
    {
        // 미사일 파괴
        gameObject.SetActive(false);
    }

    // PhotonView의 IPunObservable 인터페이스 구현
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 위치 데이터 전송
            stream.SendNext(transform.position);
        }
        else
        {
            // 위치 데이터 수신 및 적용
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}
