using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int timesHit = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo) {
        if (timesHit > 5) {
            Destroy(hitInfo);
        }
        Destroy(GameObject);
    }
}
