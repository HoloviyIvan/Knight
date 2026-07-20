using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    [SerializeField] private GameObject pauseMenuUI;
    
    void Start()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            if (gameIsPause)
            {
                Resum();
            }
            else
            {
                Pause();
            }            
        }
    }

    public void Resum()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameIsPause = false;        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPause = true;        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
