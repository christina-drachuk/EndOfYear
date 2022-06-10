using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeballCOllect : MonoBehaviour
{

    bool hasGun1 = false;
    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.CompareTag("Player"))
        {
            hasGun1 = true;
            //Add coin to counter
            //totalCoins++;
            //Test: Print total number of coins
            //Debug.Log("You currently have " + SC_2DCoin.totalCoins + " Coins.");
            Destroy(gameObject);
        }
    }

    
}