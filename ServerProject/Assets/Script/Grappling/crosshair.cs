using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture; // 크로스헤어 텍스처
    public float crosshairSize = 32f; // 크로스헤어 크기
    void OnGUI()
    {
        // 마우스 위치를 얻어옴
        Vector2 mousePosition = Event.current.mousePosition;

        // 크로스헤어 위치 계산
        float xMin = mousePosition.x - (crosshairSize / 2);
        float yMin = mousePosition.y - (crosshairSize / 2);

        // 크로스헤어 그리기
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize, crosshairSize), crosshairTexture);
    }
}
