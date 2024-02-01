using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private HealthSystem healthSystem;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
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
