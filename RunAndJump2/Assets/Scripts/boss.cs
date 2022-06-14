using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{

    public float leftLimit;
    public float rightLimit;
    public float speed = 1f;
    public bool moveRight = true;
    public float timePass;

    void Start()
    {
        leftLimit = 4.97f;
        rightLimit = 8.66f;
        
    }

    public void Update()
    {
        if (moveRight) {
            if (transform.position.x <= rightLimit) {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else {
                moveRight = false;
            }
        }
        else  {
            if (transform.position.x >= leftLimit) {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            else {
                moveRight = true;
            }
        }
        timePass += Time.deltaTime;
        if (timePass > 1000) {

            timePass = 0;
        }

    }
}