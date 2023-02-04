using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction, _clickPoint;
    public float speed, bulletLifetime;

    public void Init(Player p, float xPos)
    {
        this.transform.localPosition = new Vector3(xPos, 0, 0);
        StartCoroutine(End());
    }

    void Update()
    {
        
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(bulletLifetime);
        Destroy(this.gameObject);
    }
}
