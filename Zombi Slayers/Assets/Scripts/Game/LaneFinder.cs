using UnityEngine;

public class LaneFinder : MonoBehaviour
{
    public static float[] laneYPositions = { -24f, -3f, 0.25f, 3.5f };

    [SerializeField] private bool snap;
    public int lane;

    private void OnValidate()
    {
        SnapToNearestLaneY();
        snap = false;
        if (snap) { /*_*/ }
    }

    void SnapToNearestLaneY()
    {
        float closestY = laneYPositions[0]; // Ba�lang��ta ilk de�eri al�yoruz
        float closestDistance = Mathf.Abs(transform.position.y - closestY); // �lk de�erin fark�n� hesapla

        // En yak�n Y de�erini bul
        for (int i = 1; i < laneYPositions.Length; i++)
        {
            float currentDistance = Mathf.Abs(transform.position.y - laneYPositions[i]);
            if (currentDistance < closestDistance)
            {
                closestY = laneYPositions[i];
                closestDistance = currentDistance;
            }
        }

        // En yak�n Y de�erine snap et
        transform.position = new Vector3(transform.position.x, closestY, transform.position.z);

        if (laneYPositions[1] == transform.position.y) { lane = 1; }
        if (laneYPositions[2] == transform.position.y) { lane = 2; }
        if (laneYPositions[3] == transform.position.y) { lane = 3; }
    }

    public void MakeLane(int laner)
    {
        lane = laner;
    }
}
