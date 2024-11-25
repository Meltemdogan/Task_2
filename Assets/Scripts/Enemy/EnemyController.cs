using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] HealthBar HealthBar;
    
    private Transform player => PlayerController.Instance.transform;
    public float stoppingDistance = 1.0f;
    
    private void OnEnable()
    {
        HealthBar.initialize(enemyData.health);
    }
    
    private void Update()
    {
        if (player == null) return; 
        
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f; 
        transform.rotation = Quaternion.LookRotation(directionToPlayer);
        
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, enemyData.speed * Time.deltaTime);
        }
    }
    
    public void TakeDamage(float damage)
    {
        HealthBar.TakeDamage(damage);
        if (HealthBar.CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameManager.Instance.AddScore(enemyData.points);
        ObjectPoolManager.Instance.ReturnObject(enemyData.enemyName, gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
           PlayerController.Instance.TakeDamage(enemyData.damage);
        }
    }
}
