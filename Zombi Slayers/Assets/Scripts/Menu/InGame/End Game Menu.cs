using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private Button restart, next;
    public Image nextImage;
    public TextMeshProUGUI nextText;
    public bool isItAWin;
    private void OnEnable()
    {
        OpenMenu();
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

        if (next.interactable)
        {
            Color spriteColor = nextImage.color;
            spriteColor.a = 1;
            nextImage.color = spriteColor;

            Color textColor = nextText.color;
            textColor.a = 1;
            nextText.color = textColor;
        }
        else
        {
            Color spriteColor = nextImage.color;
            spriteColor.a = 0.5f;
            nextImage.color = spriteColor;

            Color textColor = nextText.color;
            textColor.a = 0.5f;
            nextText.color = textColor;
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
        int level = FindAnyObjectByType<LevelMaker>().level;
        if (level == 10)
        {
            SceneManager.LoadScene("Game End Credits");
        }
        else
        {
            SceneManager.LoadScene("level_" + (level + 1));
        }
    }
    public void ButtonSelectLevel()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Level Menu");
    }
}
