using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPlayer : Health
{
    [SerializeField] string nameLevel;
    public override void Die()
    {
        SceneManager.LoadScene(nameLevel);
    }
}
