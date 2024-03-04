using UnityEngine;
using Photon.Pun;

public class SceneManagement : MonoBehaviour
{
    public string sceneName;
    // �� ��ȯ �Լ�
    public void Update()
    {
        // ���� �ִ� ��� �÷��̾ ������
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        bool allDead = true;
        foreach (GameObject player in players)
        {
            // �� �÷��̾��� ���¸� Ȯ���Ͽ� isDead�� false�� ��찡 �ϳ��� ������ ��ȯ���� ����
            if (!player.GetComponent<Movement>().isDead)
            {
                allDead = false;
                break;
            }
        }

        // ��� �÷��̾ ����� ��쿡�� ���� ��ȯ
        if (allDead)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
