using UnityEngine;

public class CameraTargetGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public Vector2 size = new Vector2(2f, 2f); // Kamera çerçevesinin boyutu

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
