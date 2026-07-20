using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string nameLevel;
    public void LoadScene()
    {
        SceneManager.LoadScene(nameLevel);
    }
}
