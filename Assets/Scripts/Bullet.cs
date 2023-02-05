using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction;
    public float speed, bulletLifetime;
    private Player _p;
    private SpriteRenderer _sprite;

    public void Init(Player p, bool flipX, Vector2 target)
    {
        _sprite = GetComponent<SpriteRenderer>();
        float xPos = flipX ? -1 : 1;
        transform.localPosition = new Vector3(xPos, 0, 0);
        _direction = (target - (Vector2)p.transform.position).normalized;

        /*(1,0) = right (sprite default)
         (0,1) = up (+90z)
         (-1,0) = left (180z)
         (0,-1) = down (-90z)*/

        switch (_direction.x) //change so it rotates the sprite based on vector rather than flipping based on x
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
                _p = p;
        StartCoroutine(End());
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * (Vector3)_direction;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.gameObject.name);
        if(collision.collider.gameObject.tag == "enemy")
        {
            Fighter e = collision.collider.gameObject.GetComponent<Fighter>();
            e.TakeDamage(_p.attackPower);
            StopCoroutine(End());
            Destroy(this.gameObject);
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(this.gameObject);
    }
}
