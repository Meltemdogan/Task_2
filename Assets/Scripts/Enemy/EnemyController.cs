using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] Slider enemyHealthBar;
    
    private Transform player; 
    //public float speed = 3.0f; 
    public float stoppingDistance = 1.0f;
    
    private float currentHealth;
    
    PlayerHealth playerHealth;

    private void Start()
    {
        currentHealth = enemyData.health;
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
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
        //currentHealth = enemyData.health;
        //damage = enemyData.projectileDamage;
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
        Debug.Log("Damage: " + damage);
        if (currentHealth <= 0)
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
            //other.gameObject.GetComponent<IDamageable>().TakeDamage(enemyData.damage);
        }
    }
}
