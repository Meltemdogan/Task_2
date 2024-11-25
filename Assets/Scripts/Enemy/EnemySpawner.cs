using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    
    public float spawnRadius = 20f;
    public float safeZoneRadius = 10f;
    public float spawnInterval = 5f;
    
    [SerializeField] Transform player;
    
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float stoppingDistance = 1.0f;

    public List<EnemyData> m_Enemies;
    public List<ScoreThreshold> scoreThresholds;
    public int MaxIndex;
    public float projectileDamage;
    public float currentHealth;

    private EnemyData m_SelectedData;

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

    private void Start()
    {
        InvokeRepeating((nameof(SpawnEnemy)), 0, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if(player == null) return;
        
        UpdateMaxIndex();
        Vector3 spawnPosition = GetRandomSpawnPosition();
        
        m_SelectedData = m_Enemies[Random.Range(0, MaxIndex)];
        projectileDamage = m_Enemies[MaxIndex-1].projectileDamage;
        currentHealth = m_Enemies[MaxIndex-1].health;
        
        GameObject enemy = ObjectPoolManager.Instance.GetObject(m_SelectedData.enemyName);
        
        if(enemy != null)
        {
            enemy.transform.position = spawnPosition;
            enemy.SetActive(true);
            transform.LookAt(player);
        }
        else
        {
            Debug.LogWarning("Enemy pool is empty!");
        }
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition;
        do
        { 
            float randomAngle = Random.Range(0f, 360f);
            Vector3 direction = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));
            
            spawnPosition = player.position + direction * Random.Range(safeZoneRadius, spawnRadius);
        } 
        while (Vector3.Distance(spawnPosition, player.position) < safeZoneRadius);

        return spawnPosition;
    }
    public void UpdateMaxIndex()
    {
        int totalScore = GameManager.Instance.totalScore;
        foreach (var threshold in scoreThresholds)
        {
            if (totalScore <= threshold.scoreLimit)
            {
                MaxIndex = threshold.maxIndex;
                Debug.Log("Max Index: " + MaxIndex);
                return;
            }
        }
    }
}
[System.Serializable]
public class ScoreThreshold
{
    public int scoreLimit;
    public int maxIndex;
}
