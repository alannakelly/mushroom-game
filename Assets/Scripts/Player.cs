using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Fighter
{
    public Bullet bullet;
    public int mana, maxMana;
    public Camera cam;

    void Start()
    {
        this.anim = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.coll = GetComponent<Collider2D>();
        this.hurtAnim = "ShroomHurt";
        this.attackAnim = "ShroomAttack";
    }

    public void OnMove(InputValue v)
    {
        this.move = v.Get<Vector2>();
    }

    public void OnAttack()
    {
        this.Attack(0.5f);
    }

    public void OnAbsorb()
    {
        //absorb mana if on plant, otherwise do nothing
    }

    void Update()
    {
        if (this.canMove)
        {

            if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
            {
                this.TakeDamage(1);
            }
            if (this.move != Vector2.zero && this.canMove)
            {
                this.anim.SetFloat("Move", 1);
                transform.position += (Vector3)this.move * moveSpeed * Time.deltaTime;
                switch (this.move.x)
                {
                    case < 0: //moving left
                        this.sprite.flipX = true;
                        this.coll.offset = new Vector2(0.2f, 0);
                        break;

                    case > 0: //moving right
                        this.sprite.flipX = false;
                        this.coll.offset = new Vector2(-0.2f, 0);
                        break;

                    case 0: //no horizontal movement
                        break;
                }
            }
            else
            {
                this.anim.SetFloat("Move", 0);
            }
        }
    }

    public override void Attack(float animTime)
    {
        //this.attacking = true;
        //this.anim.Play("ShroomAttack", 0, 0);
        base.Attack(animTime);
        Bullet shot = Instantiate(bullet, this.transform);
        Vector2 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        shot.Init(this, this.sprite.flipX, mousePos);
        //StartCoroutine(this.AttackAnim(0.5f));
    }

    public override void TakeDamage(int damage, float animTime = 0.5f)
    {
        base.TakeDamage(damage, animTime);
    }

    public override void Die()
    {
        print("Game Over");
    }
}
