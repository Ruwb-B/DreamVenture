using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(1000, 1000);

    public GameObject _pauseMenuUI;
    public GameObject _levelCompleteUI;

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenuUI.activeSelf)
            {
                _pauseMenuUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                _pauseMenuUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void CompleteLevel()
    {
        _levelCompleteUI.SetActive(true);
    }
}
