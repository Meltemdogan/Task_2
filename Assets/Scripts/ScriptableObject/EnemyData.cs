using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Temel Özellikler")]
    public string enemyName; 
    public int health;       
    public float speed;
    public float damage;
    public int points;
}
