using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDiamond : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            ScoreDiamond.scoreDiamonds += 1;
            Destroy(gameObject);
        }
    }
}
