using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

// BU KOD CHATGPT ÝLE YAPILMIÞTIR
public class ShotgunAmmoPositioner : MonoBehaviour
{
    public float heightDifference;
    public float widthDifference;
    public float fixedXPosition;
    public int ammoCountAtOnce;
    public float ammoDegreeDifference;

    public GameObject[] ammo;

    private void Start()
    {
        if (ammo == null || ammo.Length == 0)
            return;

        bool isValid = true;

        for (int i = 0; i < ammo.Length; i++)
        {
            GameObject currentAmmo = ammo[i];
            if (currentAmmo == null)
                continue;

            bool shouldBeActive = i < ammoCountAtOnce;

            if (currentAmmo.activeSelf != shouldBeActive)
            {
                isValid = false;
                Debug.LogError($"[Ammo Setup Error] Ammo at index {i} is {(currentAmmo.activeSelf ? "active" : "inactive")} but should be {(shouldBeActive ? "active" : "inactive")}.\n" +
                    $"Please fix this manually in the Inspector by toggling the checkbox on the GameObject.");
            }
        }

        if (!isValid)
        {
            Debug.LogError("Ammo configuration incorrect! Game is paused. Check the console for instructions.");
            Time.timeScale = 0;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Diz Mermileri")]
    private void ArrangeAmmo()
    {
        if (ammo == null || ammo.Length == 0)
        {
            Debug.LogWarning("Ammo listesi boþ.");
            return;
        }

        int count = ammoCountAtOnce;
        if (count <= 0 || count > ammo.Length)
        {
            Debug.LogWarning("Ammo count hatalý ya da ammo array uzunluðunu aþýyor.");
            return;
        }

        int midIndex = count / 2;

        for (int i = 0; i < count; i++)
        {
            GameObject currentAmmo = ammo[i];
            if (currentAmmo == null)
                continue;

            int offsetFromCenter = i - midIndex;

            float yPos = (count % 2 == 0)
                ? (offsetFromCenter + 0.5f) * heightDifference
                : offsetFromCenter * heightDifference;

            float distanceFromCenter = Mathf.Abs(offsetFromCenter + (count % 2 == 0 ? 0.5f : 0f));
            float normalizedDistance = 1f - (distanceFromCenter / (count / 2f));
            float xPos = fixedXPosition + widthDifference * normalizedDistance;

            float zRot = offsetFromCenter * ammoDegreeDifference;

            currentAmmo.transform.localPosition = new Vector3(xPos, yPos, 0f);
            currentAmmo.transform.localRotation = Quaternion.Euler(0f, 0f, zRot);

            // Editor'de deðiþikliði kaydetmesi için iþaretle
            EditorUtility.SetDirty(currentAmmo.transform);
        }

        Debug.Log("Mermiler dizildi. Gerekirse 'Apply' yapmayý unutma.");
    }
#endif



}
