using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject RoomUI; // Room UI 오브젝트
    public GameObject JoinUI; // Join UI 오브젝트

    // Room UI를 활성화 시키는 매서드 (버튼 UI)
    public void SetActiveRoomUI()
    {
        RoomUI.SetActive(true);
    }

    // Room UI를 비활성화 시키는 매서드 (버튼 UI)
    public void SetDisactiveRoomUI()
    {
        RoomUI.SetActive(false);
    }

    // Join UI를 비활성화 시키는 매서드 (버튼 UI)
    public void SetActiveJoinUI()
    {
        JoinUI.SetActive(true);
    }

    // Join UI를 비활성화 시키는 매서드 (버튼 UI)
    public void SetDisativeJoinUI()
    {
        JoinUI.SetActive(false);
    }
}
