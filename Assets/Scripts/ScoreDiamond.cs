using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDiamond : MonoBehaviour
{
    public static int scoreDiamonds;
    [SerializeField] Text scoreText;
    [SerializeField] int numberDiamondPerLevel;

    void Start()
    {
        scoreDiamonds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{scoreDiamonds}/{numberDiamondPerLevel}";
    }
}
