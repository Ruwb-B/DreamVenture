using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public GameObject _mainMenu;

    public void PlayGameLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void Quit()
    {
        _mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
