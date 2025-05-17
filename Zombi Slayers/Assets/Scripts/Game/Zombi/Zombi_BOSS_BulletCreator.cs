using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi_BOSS_BulletCreator : MonoBehaviour
{
    [SerializeField] private Zombi_BOSS boss;
    [SerializeField] private GameObject _spit;
    [SerializeField] private GameObject _zombiBall;
    public void CreateSpit()
    {
        if (boss.lane == 1)
        {
            Instantiate(_spit, new Vector3(transform.position.x, LaneFinder.laneYPositions[2], transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(_spit, new Vector3(transform.position.x, LaneFinder.laneYPositions[3], transform.position.z), Quaternion.identity);
        }
    }
    public void CreateZombiBall()
    {
        if (boss.lane == 1)
        {
            Instantiate(_zombiBall, new Vector3(transform.position.x, LaneFinder.laneYPositions[1], transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(_zombiBall, new Vector3(transform.position.x, LaneFinder.laneYPositions[2], transform.position.z), Quaternion.identity);
        }
    }
}
