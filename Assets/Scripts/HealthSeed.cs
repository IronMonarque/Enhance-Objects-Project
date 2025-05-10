using System;
using UnityEngine;

public class HealthSeed 
{
    private float currentHealth;
    private float maxHealth;
    private float healthRegenRate;

    public Action<float> OnHealthUpdate;

    public float GetHealth()
    {
        return currentHealth;
    }

    public HealthSeed(float maxHealth, float healthRegenRate, float currentHealth = 100) //Constroctor
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.healthRegenRate = healthRegenRate;

        OnHealthUpdate?.Invoke(currentHealth);
    }
    
    public HealthSeed(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void RegenHealth()
    {
        AddHealth(healthRegenRate * Time.deltaTime);
    }

    public HealthSeed()
    {

    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + value);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void DeductHealth(float value)
    {
        currentHealth = Mathf.Max(0, currentHealth - value);
        OnHealthUpdate?.Invoke(currentHealth);
    }
}
