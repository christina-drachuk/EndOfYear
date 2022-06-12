using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUpdater: MonoBehaviour
{
//   

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }


    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.CompareTag("Player")){
            
            Destroy(gameObject);
        }

    }
    

    // // Update is called once per frame
    // void Update()
    // {
    //     coinScore.SetText("Coins: " + totalCoins);
    // }
}