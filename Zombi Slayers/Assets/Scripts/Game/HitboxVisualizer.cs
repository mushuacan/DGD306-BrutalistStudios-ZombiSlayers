using UnityEngine;

public class HitboxVisualizer2D : MonoBehaviour
{
    // BoxCollider2D component'ini referans olarak al
    public BoxCollider2D boxCollider;

    void OnDrawGizmos()
    {
        if (boxCollider == null)
            return;

        // Gizmos rengini kýrmýzý yap
        Gizmos.color = Color.red;

        // BoxCollider2D boyutunu ve pozisyonunu kullanarak bir çizim yap
        Gizmos.DrawWireCube(transform.position + (Vector3)boxCollider.offset, boxCollider.size);
    }
}
