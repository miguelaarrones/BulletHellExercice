using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image barImage;

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        barImage.fillAmount = 1f;

        Hide();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        barImage.fillAmount = healthSystem.GetHealthAmountNormalized();

        if (healthSystem.GetHealthAmountNormalized() == 1f)
        {
            Hide();
        } else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
