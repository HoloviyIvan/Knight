using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;    
    [SerializeField] HealthPlayer healthPlayer;
    [SerializeField] private Image fireBall;
    private float fill;

    private bool canFill = true;

    private void Start()
    {       
        fill = 1;    
    }

    void Update()
    {
        fill += Time.deltaTime / 2f;
        healthBar.fillAmount = healthPlayer.HealthPoints*0.1f;
        if (Input.GetKeyDown(KeyCode.Mouse1) && canFill)
        {
            canFill = false;
            fireBall.fillAmount = 0;          
            fill = 0;
            StartCoroutine(CanFill());
        }

        fireBall.fillAmount = fill;        
    }

    private IEnumerator CanFill()
    {
        yield return new WaitForSeconds(2f);
        canFill = true;
    }

}
