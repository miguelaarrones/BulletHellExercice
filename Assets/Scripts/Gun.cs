using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            // Hit something!
            Enemy enemy;
            hit.collider.gameObject.TryGetComponent(out enemy);
            if (enemy != null) // Check if enemy
            {
                // It's an enemy.
                enemy.Damage(damage);
            }
        }
    }
}
