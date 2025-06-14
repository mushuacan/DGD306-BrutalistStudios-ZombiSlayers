using UnityEngine;
using TMPro;

public class TMPJumpText : MonoBehaviour
{
    public TMP_Text textComponent;
    public float amplitude = 10f;   // Zýplama yüksekliði
    public float speed = 5f;        // Zýplama hýzý

    TMP_TextInfo textInfo;

    void Start()
    {
        textComponent.ForceMeshUpdate();
        textInfo = textComponent.textInfo;
    }

    void Update()
    {
        textComponent.ForceMeshUpdate();
        textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            int vertexIndex = charInfo.vertexIndex;
            int materialIndex = charInfo.materialReferenceIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            // Time.unscaledTime kullan --> TimeScale etkilenmesin
            float offsetY = Mathf.Sin(Time.unscaledTime * speed + i * 0.5f) * amplitude;

            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j].y += offsetY;
            }
        }

        // Mesh'i güncelle
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
