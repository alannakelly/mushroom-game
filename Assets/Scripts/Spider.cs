using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Fighter
{
    void Start()
    {
        this.anim = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.coll = GetComponent<Collider2D>();
        this.hurtAnim = "SpiderHurt";
        this.attackAnim = "SpiderAttack";
    }

    void Update()
    {
        if (this.move != Vector2.zero && this.canMove)
        {
            this.anim.SetFloat("Move", 1);
            transform.position += (Vector3)this.move * moveSpeed * Time.deltaTime;
            switch (this.move.x)
            {
                case > 0: //moving right
                    this.sprite.flipX = true;
                    this.coll.offset = new Vector2(-0.2f, -0.3f);
                    break;

                case < 0: //moving left
                    this.sprite.flipX = false;
                    this.coll.offset = new Vector2(0.2f, -0.3f);
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

    public override void TakeDamage(int damage, float animTime = 0.5f)
    {
        base.TakeDamage(damage, animTime);
    }
}
