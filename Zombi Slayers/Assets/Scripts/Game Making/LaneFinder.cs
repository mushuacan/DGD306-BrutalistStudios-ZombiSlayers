using UnityEngine;

public class LaneFinder : MonoBehaviour
{
    public static float[] laneYPositions =
    //{ -24f, -3f, 0.25f, 3.5f };
    //{ -24f, -3.2f, -0.3f, 2.56f };
    { -24f, -4.12f, -1.08f, 1.96f };

    [SerializeField] private bool snap;
    public int lane;

    private void OnValidate()
    {
        SnapToNearestLaneY();
        snap = false;
        if (snap) { /*_*/ }
    }

    public void SnapTheLane()
    {
        SnapToNearestLaneY();
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
