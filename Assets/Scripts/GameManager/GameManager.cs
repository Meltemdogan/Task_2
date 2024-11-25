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
    }
    
    private void Update()
    {
        scoreText.text = "Score: " + totalScore;
    }
    
    public void AddScore(int points)
    {
        totalScore += points;
        Debug.Log("Score: " + totalScore);
    }
}
