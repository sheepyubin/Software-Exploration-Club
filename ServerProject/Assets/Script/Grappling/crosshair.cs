using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture; // ũ�ν���� �ؽ�ó
    public float crosshairSize = 32f; // ũ�ν���� ũ��
    void OnGUI()
    {
        // ���콺 ��ġ�� ����
        Vector2 mousePosition = Event.current.mousePosition;

        // ũ�ν���� ��ġ ���
        float xMin = mousePosition.x - (crosshairSize / 2);
        float yMin = mousePosition.y - (crosshairSize / 2);

        // ũ�ν���� �׸���
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize, crosshairSize), crosshairTexture);
    }
}
