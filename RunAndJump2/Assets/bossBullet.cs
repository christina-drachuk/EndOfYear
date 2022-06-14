// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class bossBullet : MonoBehaviour
{
    public float speed = -15f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {

        if (c2d.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
