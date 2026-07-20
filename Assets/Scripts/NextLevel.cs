using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : LoadLevel
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
