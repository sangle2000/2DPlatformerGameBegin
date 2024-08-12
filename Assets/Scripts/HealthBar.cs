using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Heart playerHeart;
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image totalHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHeart.CurrentHeart / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHeart.CurrentHeart / 10;
    }
}
