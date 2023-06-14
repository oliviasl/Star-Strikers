using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int scoreIncrement = 50;

    private int score;
    private static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void Start()
    {
        score = 0;
    }

    public void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore()
    {
        score += scoreIncrement;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
