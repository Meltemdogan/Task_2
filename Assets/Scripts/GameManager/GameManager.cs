using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private int score = 0;
    public int totalScore;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text accumulatedScoreText;
    private float AccumulatedScore
    {
        get => PlayerPrefs.GetFloat("AccumulatedScore", 0);
        set => PlayerPrefs.SetFloat("AccumulatedScore", value);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        accumulatedScoreText.text = "Accumulated Score: " + AccumulatedScore;
    }

    private void Update()
    {
        scoreText.text = "Score: " + totalScore;
        accumulatedScoreText.text = "Total Score: " + AccumulatedScore;
    }
    
    public void AddScore(int points)
    {
        totalScore += points;
        AccumulatedScore += points;
        PlayerPrefs.Save();
        Debug.Log("Score: " + totalScore);
    }
}
