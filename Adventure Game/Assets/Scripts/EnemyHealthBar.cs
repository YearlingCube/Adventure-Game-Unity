﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Enemy Health Bar
public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;

    }
    public void SetHealth(int Health)
    {
        slider.value = Health;
    }
}