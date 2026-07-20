using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 5;

    public event Action onDieAction;

    public int HealthPoints
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }
        }
    }


    public void Hit(int damage)
    {

        HealthPoints -= damage;
        StartCoroutine(DamageAnimation());
    }

    public virtual void Die()
    {
        onDieAction?.Invoke();
        Destroy(gameObject);
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
