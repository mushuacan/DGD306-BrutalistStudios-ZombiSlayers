using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndCredits : MonoBehaviour
{
    public GameObject MovingObject;
    public Image fadeImage;
    public string nextSceneName = "Level Menu"; // Yüklemek istediðin sahne adý
    public AudioClip[] clip;

    void Start()
    {
        All_Musician.Instance.PlayMusicSmoothly(clip);

        Sequence sequence = DOTween.Sequence();

        RectTransform rectTransform = MovingObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;

        sequence
            .AppendInterval(2.4f)
            .Append(rectTransform.DOAnchorPosY(524, 8.88f).SetEase(Ease.InOutSine))
            .AppendInterval(2.4f)
            .Append(rectTransform.DOAnchorPosY(2224, 24f).SetEase(Ease.InOutSine))
            .AppendInterval(2.4f)
            .Append(rectTransform.DOAnchorPosY(5424, 44.44f).SetEase(Ease.InOutSine))
            .AppendInterval(2.4f);

        sequence.OnComplete(() =>
        {
            FadeAndLoadScene();
        });

    }
    public void FadeAndLoadScene()
    {
        fadeImage.raycastTarget = true;

        fadeImage.DOFade(1f, 0.98f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(nextSceneName);
            });
    }
}
