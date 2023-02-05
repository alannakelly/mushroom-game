using System;
using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Tooltip("Current health.")] public int health;
    [Tooltip("Maximum possible health.")] public int maxHealth;
    [Tooltip("Distance units moved per second.")] public float moveSpeed;
    [Tooltip("Damage dealt by attack.")] public int attackPower;
    [SerializeField][Tooltip("Duration of attack animation.")] private float _attackTime;
    public AudioClip hurtSound, attackSound, deathSound;
    [NonSerialized] public AudioSource sound;
    [NonSerialized] public bool hurt, attacking;
    [NonSerialized] public Vector2 move;
    [NonSerialized] public SpriteRenderer sprite;
    [NonSerialized] public Animator anim;
    [NonSerialized] public Collider2D coll;
    [NonSerialized] public string hurtAnim, attackAnim;
    public bool canMove
    {
        get
        {
            return (!this.hurt && !this.attacking);
        }
    }

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
        this.hurt = true;
        this.sound.PlayOneShot(this.hurtSound);
        this.anim.Play(this.hurtAnim, 0, 0);
        AnimatorClipInfo[] info = this.anim.GetCurrentAnimatorClipInfo(0);
        float t = info[0].clip.length;
        StartCoroutine(this.HurtAnim(t));
        if (this.health <= 0)
        {
            this.Die();
        }
    }

    public IEnumerator HurtAnim(float t)
    {
        yield return new WaitForSeconds(t);
        this.hurt = false;
    }

    public virtual void Attack()
    {
        this.attacking = true;
        this.anim.Play(this.attackAnim, 0, 0);
        AnimatorClipInfo[] info = this.anim.GetCurrentAnimatorClipInfo(0);
        float t = info[0].clip.length;
        StartCoroutine(this.AttackAnim(t));
    }

    public IEnumerator AttackAnim(float t)
    {
        yield return new WaitForSeconds(t);
        this.attacking = false;
    }

    public virtual void Die()
    {
        this.sound.PlayOneShot(this.deathSound);
        Destroy(this.gameObject, deathSound.length);
    }
}
