using UnityEngine;
using DG.Tweening;

public class ZombiAtTheBack : MonoBehaviour
{
    public float spaceBetweenZombis;
    public SpriteRenderer resim;
    private float[] laneYPoz = { -24f, -4.12f, -1.08f, 1.96f };

    public void SetPosition(int lane, int order, float lanerXPosition)
    {
        float orderPosition = lanerXPosition - order * spaceBetweenZombis;
        transform.position = new Vector3(orderPosition, laneYPoz[lane], 0);
        resim.sortingOrder = 10 + order;
    }
}
