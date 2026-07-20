using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject fireBallEffect;

    private void Start()
    {
        StartCoroutine(DestroyFireBall());
    }

    void Update()
    {
        rigidbody2D.velocity = transform.right * speed;     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Health>();
        if (player != null)
        {            
            player.Hit(damage);
            FireBallEffect();
            Destroy(gameObject);            
        }        
    }

    IEnumerator DestroyFireBall()
    {
        yield return new WaitForSeconds(1.5f);
        FireBallEffect();
        Destroy(gameObject);
    }

    private void FireBallEffect()
    {
        Instantiate(fireBallEffect, transform.position, transform.rotation);
    }
}
