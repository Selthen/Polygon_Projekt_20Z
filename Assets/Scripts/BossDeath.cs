﻿using UnityEngine;
using UnityEngine.Assertions;


public class BossDeath : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1000.0f)] private float radius = 7.0f;
    [SerializeField, Range(0.0f, 1000.0f)] private float power = 500.0f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject effectHandler;
    private HealthComponent health;


    private void Awake()
    {
        Assert.IsNotNull(explosionEffect);
        Assert.IsNotNull(effectHandler);
        health = GetComponent<HealthComponent>();
        Assert.IsNotNull(health);
        health.OnDeath += Explode;
    }

    private void Explode()
    {
        effectHandler = Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(var collider in colliders)
        {
            HealthComponent health = collider.GetComponent<HealthComponent>();
            if(health != null)
            {
                health.ApplyDamage((radius - Vector3.Distance(collider.transform.position, transform.position)) * power);
            }
        }
        Destroy(effectHandler, 2.0f);
    }
}