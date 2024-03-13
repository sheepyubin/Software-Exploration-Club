using Photon.Pun;
using UnityEngine;

public class PlayerColor : MonoBehaviourPunCallbacks
{
    SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        Color color = SetRandomColor();

        photonView.RPC("SyncColor", RpcTarget.OthersBuffered, color.r, color.g, color.b, color.a); // ���� ����
    }

    // ���� ������ ��ȯ�ϴ� �ż���
    Color SetRandomColor()
    {
        // ���� RGB �� ����
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = 1f; // Alpha

        Color color = new Color(r, g, b, a);

        return color;
    }

    [PunRPC]
    void SyncColor(float r, float g, float b, float a)
    {
        Color color = new Color(r, g, b, a);
        sp.color = color;
    }
}
