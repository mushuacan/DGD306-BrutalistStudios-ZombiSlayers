using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndGameImage : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite[] backgroundSprite;
    public void StartFlashEffect(float sucsess)
    {
        if (sucsess < 0.25f)
        {
            background.sprite = backgroundSprite[0];
            FlashEffect(1f, 0.5f, 0.5f, 1.5f);
        }
        else if (sucsess < 0.63f)
        {
            background.sprite = backgroundSprite[1];
            FlashEffect(0.9f, 1f, 0.25f, 1f);
        }
        else if (sucsess < 0.98f)
        {
            background.sprite = backgroundSprite[2];
            FlashEffect(0.9f, 1f, 0.25f, 1f);
        }
        else
        {
            background.sprite = backgroundSprite[3];
            FlashEffect(0.9f, 0.5f, 0.25f, 1f);
        }
    }

    private void FlashEffect(float powerOfImage, float powerTimer, float powerOfRemaining, float remainingTimer)
    {
        //background.color = Color.white;
        background.DOFade(powerOfImage, powerTimer).SetEase(Ease.OutQuad) //0.9f, 1f
            .OnComplete(() =>
            {
                background.DOColor(Color.black, remainingTimer);
                background.DOFade(powerOfRemaining, remainingTimer).SetEase(Ease.InQuad); //0.25f, 1f
            });
    }
}
