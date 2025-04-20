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
        float closestY = laneYPositions[0]; // Baþlangýçta ilk deðeri alýyoruz
        float closestDistance = Mathf.Abs(transform.position.y - closestY); // Ýlk deðerin farkýný hesapla

        // En yakýn Y deðerini bul
        for (int i = 1; i < laneYPositions.Length; i++)
        {
            float currentDistance = Mathf.Abs(transform.position.y - laneYPositions[i]);
            if (currentDistance < closestDistance)
            {
                closestY = laneYPositions[i];
                closestDistance = currentDistance;
            }
        }

        // En yakýn Y deðerine snap et
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
