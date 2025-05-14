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
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(next.gameObject);
    }
    public void ButtonRestart()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ButtonNext()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Level Menu");
    }
}
