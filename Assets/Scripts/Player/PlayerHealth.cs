using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private float maxPlayerHealth = 100;
    [SerializeField] private float currentPlayerHealth;
    [SerializeField] public Slider playerHealthBar;
    
    private void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
    }

    public void TakeDamage(float damage, float currentHealth)
    {
        //damage = EnemySpawner.Instance.enemyDamage; 
        currentPlayerHealth -= damage;
        playerHealthBar.value = currentPlayerHealth;
        if (currentPlayerHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
