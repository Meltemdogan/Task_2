using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float CurrentHealth;
    [SerializeField] public Slider playerHealthBar;
    
    public void initialize(float health)
    {
        CurrentHealth = health;
        playerHealthBar.maxValue = health;
        playerHealthBar.value = health;
    }
    
    private void LateUpdate()
    {
        var _rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        transform.rotation = _rotation;
    }
    
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        playerHealthBar.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
