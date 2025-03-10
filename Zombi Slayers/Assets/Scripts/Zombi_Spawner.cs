using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_Spawner : MonoBehaviour
{
    public GameObject zombiPrefab;
    public float[] laneYPositions;

    // Start is called before the first frame update
    void Start()
    {
        Spawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawner ()
    {
        DOVirtual.DelayedCall(3f, () =>
        {
            GameObject zombi = Instantiate(zombiPrefab);
            int randomNumber = Random.Range(1, 4);
            zombi.transform.position = new Vector2(10, laneYPositions[randomNumber]);
            Spawner();
        });
    }
}
