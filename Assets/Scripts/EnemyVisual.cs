using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Material onHitMaterial;
    [SerializeField] private float onHitTimerMax = .5f;

    private MeshRenderer meshRenderer;
    private Material originalMaterial;

    private float onHitTimer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        originalMaterial = meshRenderer.material;
    }

    private void Update()
    {
        if (meshRenderer.material != originalMaterial)
        {
            if (onHitTimer >= onHitTimerMax)
            {
                meshRenderer.material = originalMaterial;
                onHitTimer = 0;
            }
            else
            {
                onHitTimer += Time.deltaTime;
            }
        }
    }
}
