using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Fighter
{
    private bool _attacking, _hurt;
    private bool _canMove
    {
        get
        {
            return (!_attacking && !_hurt);
        }
    }
    private Animator _anim;
    private SpriteRenderer _sprite;
    public Bullet bullet;
    public int mana, maxMana;
    private Vector2 _move;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue v)
    {
        _move = v.Get<Vector2>();
    }

    public void OnAttack()
    {
        _anim.Play("ShroomAttack", 0, 0);
        StartCoroutine(Attack());
    }

    public void OnAbsorb()
    {
        //absorb mana if on plant, otherwise do nothing
    }

    void Update()
    {
        if (_canMove)
        {
            if (_move != Vector2.zero)
            {
                _anim.SetFloat("Anim", 1);
                transform.position += (Vector3)_move * moveSpeed * Time.deltaTime;
                switch (_move.x)
                {
                    case < 0: //moving left
                        _sprite.flipX = true;
                        break;

                    case > 0: //moving right
                        _sprite.flipX = false;
                        break;

                    case 0: //no horizontal movement
                        break;
                }
            }
            else
            {
                _anim.SetFloat("Anim", 0);
            }
        }
    }

    public IEnumerator Attack()
    {
        Bullet shot = Instantiate(this.bullet, this.transform);
        float xPos = _sprite.flipX ? -1 : 1;
        shot.Init(this, xPos);
        yield return new WaitForSeconds(0.5f);
        _attacking = false;
    }

    public override void TakeDamage(int damage)
    {
        _hurt = true;
        _anim.Play("ShroomHurt", 0, 0);
        base.TakeDamage(damage);
        StartCoroutine(HurtAnim());
    }

    public IEnumerator HurtAnim()
    {
        yield return new WaitForSeconds(0.5f);
        _hurt = false;
    }

    public override void Die()
    {
        //game over screen
    }
}
