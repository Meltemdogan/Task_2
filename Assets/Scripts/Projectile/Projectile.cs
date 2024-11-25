using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float ProjectileDamage = 2.5f;
    EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyController enemyController))
        {
            gameObject.SetActive(false);
            
            enemyController.TakeDamage(ProjectileDamage);
        }  
        ObjectPoolManager.Instance.ReturnObject("Projectile", gameObject);
    }
}
