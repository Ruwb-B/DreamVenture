using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _levelMenu;
    public GameObject _optionMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowOptionMenu()
    {
        _optionMenu.SetActive(true);
    }

    public void ShowLevelsMenu()
    {
        _levelMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
