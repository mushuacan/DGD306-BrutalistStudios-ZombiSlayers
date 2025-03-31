using UnityEngine;

public class HitboxVisualizer2D : MonoBehaviour
{
    // BoxCollider2D component'ini referans olarak al
    public BoxCollider2D boxCollider;

    void OnDrawGizmos()
    {
        if (boxCollider == null)
            return;

        // Gizmos rengini k�rm�z� yap
        Gizmos.color = Color.red;

        // BoxCollider2D boyutunu ve pozisyonunu kullanarak bir �izim yap
        Gizmos.DrawWireCube(transform.position + (Vector3)boxCollider.offset, boxCollider.size);
    }
}
