using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    [SerializeField] private float healthAmountMax = 100f;
    private float healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead() => healthAmount == 0;

    public float GetHealthAmount() => healthAmount;

    public float GetHealthAmountNormalized() => (float)healthAmount / healthAmountMax;
}
