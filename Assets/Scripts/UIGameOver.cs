using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    void Start()
    {
        scoreText.text = "YOU SCORED\n" + scoreKeeper.GetScore().ToString();
    }
}
