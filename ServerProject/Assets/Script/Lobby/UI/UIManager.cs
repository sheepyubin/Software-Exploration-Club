using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject RoomUI; // Room UI ������Ʈ
    public GameObject JoinUI; // Join UI ������Ʈ

    // Room UI�� Ȱ��ȭ ��Ű�� �ż��� (��ư UI)
    public void SetActiveRoomUI()
    {
        RoomUI.SetActive(true);
    }

    // Room UI�� ��Ȱ��ȭ ��Ű�� �ż��� (��ư UI)
    public void SetDisactiveRoomUI()
    {
        RoomUI.SetActive(false);
    }

    // Join UI�� ��Ȱ��ȭ ��Ű�� �ż��� (��ư UI)
    public void SetActiveJoinUI()
    {
        JoinUI.SetActive(true);
    }

    // Join UI�� ��Ȱ��ȭ ��Ű�� �ż��� (��ư UI)
    public void SetDisativeJoinUI()
    {
        JoinUI.SetActive(false);
    }
}
