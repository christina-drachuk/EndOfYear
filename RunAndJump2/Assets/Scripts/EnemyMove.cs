using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions; 
    
    private int index;
    private SpriteRenderer _renderer;


    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
    
        if(transform.position == positions[index]){
            _renderer.flipX = false;
            if(index == positions.Length -1){
                index = 0;
                _renderer.flipX = true;
            }
            else{
                index++;

            }
        }


    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Bullet")){
            
            Destroy(gameObject);
        }

    }
}

