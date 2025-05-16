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
    public bool isItAWin;
    private void OnEnable()
    {
        if (isItAWin)
        {
            next.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(next.gameObject);
        }
        else
        {
            next.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(restart.gameObject);
            LevelMaker levelMaker = FindObjectOfType<LevelMaker>();
            if ((int)GameSettings.Instance.settings["level"] >= levelMaker.level)
            {
                next.gameObject.SetActive(true);
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
        SceneManager.LoadScene("level_" + ((int)GameSettings.Instance.settings["level"] + 1));
    }
    public void ButtonSelectLevel()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Level Menu");
    }
}
