using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Tooltip("Current health.")] public int health;
    [Tooltip("Maximum possible health.")] public int maxHealth;
    [Tooltip("Distance units moved per second.")] public float moveSpeed;
    [Tooltip("Damage dealt by attack.")] public int attackPower;
    [SerializeField] [Tooltip("Duration of attack animation.")] private float _attackTime;

    void Start()
    {
        this.health = this.maxHealth;
    }

    void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.Die();
        }
    }

    public virtual void Die()
    {

    }
}
