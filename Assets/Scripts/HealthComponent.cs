﻿using UnityEngine;
using UnityEngine.Assertions;


public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10.0f;
    private float currentHealth = 0.0f;


    public event System.Action OnDeath;


    public bool IsAlive => currentHealth > 0.0f;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float amount)
    {
        Assert.IsTrue(amount > 0.0f);
        if (IsAlive)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0.0f);
            if (currentHealth <= 0.0f)
            {
                OnDeath?.Invoke();
                Destroy(gameObject);
            }  
        }
    }

    public void ApplyHeal(float amount)
    {
        Assert.IsTrue(amount > 0.0f);
        if (IsAlive)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }
}
