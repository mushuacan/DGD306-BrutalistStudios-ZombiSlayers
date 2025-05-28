using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private Button restart, next;
    [SerializeField] private Image background;
    public bool isItAWin;
    private void OnEnable()
    {
        OpenMenu();
        FlashEffect();
    }

    private void FlashEffect()
    {
        // Önce alfa deðerini 1'e çýkar, sonra 0.25'e indir
        background.DOFade(1f, 1f) // Alfa'yý 1.0 yap, 0.1 saniyede
            .OnComplete(() =>
            {
                background.DOFade(0.25f, 1f).OnComplete(() =>
                {
                    
                });
            });
    }
    private void OpenMenu()
    {
        if (isItAWin)
        {
            next.interactable = true;
            EventSystem.current.SetSelectedGameObject(next.gameObject);
        }
        else
        {
            next.interactable = false;
            EventSystem.current.SetSelectedGameObject(restart.gameObject);
            LevelMaker levelMaker = FindObjectOfType<LevelMaker>();
            if ((int)GameSettings.Instance.settings["level"] >= levelMaker.level)
            {
                next.interactable = true;
            }
        }
    }
    public void ButtonRestart()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ButtonNext()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("level_" + (FindAnyObjectByType<LevelMaker>().level + 1));
    }
    public void ButtonSelectLevel()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Level Menu");
    }
}
