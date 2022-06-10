using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUpdater: MonoBehaviour
{
//   public int coins = 0; // the variable to display
    public TMP_Text coinScore; // The TextMeshPro object to display
    int totalCoins = 0;

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

            totalCoins++;
            Debug.Log("You currently have " + totalCoins + " Coins.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinScore.SetText("Coins: " + totalCoins);
    }
}