using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Die();
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        ObjectPoolManager.Instance.ReturnObject("Enemy", gameObject);
    }
}
