using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintUI : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] private Image barImage;


    private void Start()
    {
        playerController.OnSprintAmountChanged += PlayerController_OnSprintAmountChanged;

        barImage.fillAmount = 1f;
    }

    private void PlayerController_OnSprintAmountChanged(object sender, PlayerController.OnSprintAmountChangedArgs args)
    {
        barImage.fillAmount = args.sprintAmountNormalized;
    }
}
