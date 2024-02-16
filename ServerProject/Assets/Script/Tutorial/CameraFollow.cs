using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothing = 0.2f;
    public Vector2 minCameraBoundary;
    public Vector2 maxCameraBoundary;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 minBoundsGizmo = new Vector3(minCameraBoundary.x, minCameraBoundary.y, transform.position.z);
        Vector3 maxBoundsGizmo = new Vector3(maxCameraBoundary.x, maxCameraBoundary.y, transform.position.z);

        Vector3 topLeft = minBoundsGizmo;
        Vector3 topRight = new Vector3(maxCameraBoundary.x, minCameraBoundary.y, transform.position.z);
        Vector3 bottomLeft = new Vector3(minCameraBoundary.x, maxCameraBoundary.y, transform.position.z);
        Vector3 bottomRight = maxBoundsGizmo;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    private void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
