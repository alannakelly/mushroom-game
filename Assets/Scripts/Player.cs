using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.Animations;
using UnityEngine;

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
    public GameObject attackObj;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.TakeDamage(1);
        }
        if (Input.GetAxis("Skill1") > 0)
        {
            if (!_attacking)
            {
                _attacking = true;
                _anim.Play("ShroomAttack", 0, 0);
                StartCoroutine(Attack());
            }
        }
        else if (_canMove)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                float x = Input.GetAxis("Horizontal") * this.moveSpeed * Time.deltaTime;
                float y = Input.GetAxis("Vertical") * this.moveSpeed * Time.deltaTime;
                _anim.SetFloat("Anim", 1);
                transform.position += new Vector3(x, y, 0);
                #region change facing direction
                switch (x)
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
                #endregion
            }
            //play idle anim if player can move but isn't
            else
            {
                _anim.SetFloat("Anim", 0);
            }
        }
    }

    public IEnumerator Attack()
    {
        //instantiate attackObj
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
