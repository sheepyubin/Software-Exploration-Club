using UnityEngine;
using Photon.Pun;

public class SceneManagement : MonoBehaviour
{
    public string sceneName;
    public PlayerContainer container;
    // �� ��ȯ �Լ�
    public void Update()
    {
        bool allDead = container.CheckAllDead();

        // ��� �÷��̾ ����� ��쿡�� ���� ��ȯ
        if (allDead)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
