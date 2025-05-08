using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Endless : MonoBehaviour
{
    [Header("Ayarlamalar")]
    [SerializeField] private float speedIncreaseRate;
    [SerializeField] private float speedIncreaseWait;

    [Header("Referanslar")]
    [SerializeField] private Envoriment_Movement_Endless platform1;
    [SerializeField] private Envoriment_Movement_Endless platform2;
    [SerializeField] private Transform platform1_Prefabric;
    [SerializeField] private Transform platform2_Prefabric;

    [Header("Çýkabilenler")]
    public List<GameObject> goods = new List<GameObject>();
    public List<GameObject> neutrals = new List<GameObject>();
    public List<GameObject> bads = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        SpeedIncreaserTimer(); SpawnerTimer();
    }
    void SpeedIncreaserTimer()
    {
        DOVirtual.DelayedCall(platform1.GetEnvorimentSpeed() * 0.1f + speedIncreaseWait, () =>
        {
            SpeedIncreaser(speedIncreaseRate);
            SpeedIncreaserTimer();
        });
    }
    private void SpeedIncreaser(float value)
    {
        platform1.IncreaseMovementSpeed(value);
        platform2.IncreaseMovementSpeed(value);
    }
    void SpawnerTimer()
    {
        DOVirtual.DelayedCall(Random.Range(1f, 3f), () =>
        {
            Spawner();
            SpawnerTimer();
        });
    }
    private void Spawner()
    {
        Spawn(bads[Random.Range(0, bads.Count)], Random.Range(1, 4));
    }
    private void Spawn(GameObject obje, int lane)
    {
        GameObject createdObje = Instantiate(obje, new Vector3(30, LaneFinder.laneYPositions[lane], 0), Quaternion.identity);

        if (platform1.transform.position.x > 0)
        {
            createdObje.transform.SetParent(platform1_Prefabric);
        }
        else
        {
            createdObje.transform.SetParent(platform2_Prefabric);
        }
    }
}
