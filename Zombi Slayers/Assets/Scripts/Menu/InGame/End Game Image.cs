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
        }
        else if (sucsess < 0.63f)
        {
            background.sprite = backgroundSprite[1];
        }
        else if (sucsess < 0.98f)
        {
            background.sprite = backgroundSprite[2];
        }
        else
        {
            background.sprite = backgroundSprite[3];
        }
        FlashEffect();
    }

    private void FlashEffect()
    {
        // Önce alfa deðerini 1'e çýkar, sonra 0.25'e indir
        background.DOFade(0.9f, 1f) // Alfa'yý 1.0 yap, 0.1 saniyede
            .OnComplete(() =>
            {
                background.DOFade(0.25f, 1f).OnComplete(() =>
                {

                });
            });
    }
}
