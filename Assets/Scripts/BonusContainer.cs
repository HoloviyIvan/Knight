using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BonusContainer : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject[] bonus;

    private void Reset()
    {
        health = GetComponent<Health>();
    }
    void Start()
    {
        if (health != null)
        {
            health.onDieAction += OnDie;
            return;
        }
        Destroy(this);
    }

    private void OnDie()
    {
        if (bonus != null)
        {
            int rnd = Random.Range(0, bonus.Length);
            Instantiate(bonus[rnd], transform.position, Quaternion.identity);
        }
    }
}
