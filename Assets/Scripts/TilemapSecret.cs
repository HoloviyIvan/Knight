using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSecret : MonoBehaviour
{    
    [SerializeField] Renderer renderer;

    private float halfAlpha = 0.5f;
    private float wholeAlpha = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ChangeColor(halfAlpha, other);     
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ChangeColor(wholeAlpha, other);
    }

    private void ChangeColor(float alpha, Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        Color color = renderer.material.color;

        if (player != null)
        {
            color.a = alpha;
            renderer.material.color = color;
        }
    }
}
