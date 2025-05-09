using UnityEngine;
using DG.Tweening;

public class ZombiAtTheBack : MonoBehaviour
{
    public float spaceBetweenZombis;
    public float leftCameraBorder;
    public float zombiSpeedComesFromBack;

    private Tween myTween;

    private float[] laneYPoz = { -24f, -4.12f, -1.08f, 1.96f };

    public void SetPosition(int lane, int order)
    {
        float orderPosition = order * spaceBetweenZombis + leftCameraBorder;
        float leftEnterance = leftCameraBorder;
        float enteranceDuration = (order + 1) / zombiSpeedComesFromBack;
        transform.position = new Vector3(leftEnterance, laneYPoz[lane], 0);
        myTween = transform.DOMoveX(orderPosition, enteranceDuration).SetEase(Ease.OutQuad);
    }

    public void EndGame(float movementSpeed, float transitionDuration)
    {
        myTween.Kill();
        float xPosition = transform.position.x - (movementSpeed / 2) * transitionDuration;
        transform.DOMoveX(xPosition, transitionDuration).SetEase(Ease.InQuad);
    }
}
