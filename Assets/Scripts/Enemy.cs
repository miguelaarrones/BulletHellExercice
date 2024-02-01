using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied; 
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public void Damage(float amount)
    {
        healthSystem.Damage(amount);
    }
}
