using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _uiTutorial;
    public static GameManager Instance;
    public bool _gameIsStart;
    public bool _gameIsPause;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        };
        _gameIsStart = true;
        _mainMenu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   _mainMenu.SetActive(false);
            if (_gameIsPause)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PlayGame()
    {
        _gameIsStart = true;
        _mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _gameIsPause = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPause = true;
    }
    public void ContinueGame()
    {

        Time.timeScale = 1f;
        _gameIsPause = false;

    }
    public void Exit()
    {
        Debug.Log("_______QUIT________");
        Application.Quit();
    }

}
