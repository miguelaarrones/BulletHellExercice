using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    void Update()
    {
        TakeDamage(1f);
        Debug.Log("Health: " + health);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy(gameObject);
        Debug.Log("Die");
    }
}
