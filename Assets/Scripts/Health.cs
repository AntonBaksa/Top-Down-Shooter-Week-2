using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private string otherTag = "";
    [SerializeField] private float damage = 20;

    private float currentHealth = 100;

    public event Action OnDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            Debug.Log($"{this.name} Took Damage");
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHealth -= damage;
        Debug.Log($"Health: {currentHealth}");

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
