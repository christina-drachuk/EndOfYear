using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public int dist = 5;
    public float EnemySpeed = 500;
    public bool faceRight;
    private float startPos;
    private float endPos;

    public bool moveRight = true;


    // Use this for initialization
    public void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
        startPos = transform.position.x;
        endPos = startPos + dist;
        faceRight = transform.localScale.x > 0;
    }


// Update is called once per frame
public void Update()
{

    if (moveRight)
    {
        rb.AddForce(Vector2.right * EnemySpeed * Time.deltaTime);
        if (!faceRight)
            Flip();
    }

    if (rb.position.x >= endPos)
        moveRight = false;

    if (!moveRight)
    {
        rb.AddForce(-Vector2.right * EnemySpeed * Time.deltaTime);
        if (faceRight)
            Flip();
    }
    if (rb.position.x <= startPos)
        moveRight = true;


}

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        faceRight = transform.localScale.x > 0;
    }

}

