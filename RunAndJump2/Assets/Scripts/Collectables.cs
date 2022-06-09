using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectables : MonoBehaviour
{
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
            //Add coin to counter
            //totalCoins++;
            //Test: Print total number of coins
            //Debug.Log("You currently have " + SC_2DCoin.totalCoins + " Coins.");
            //Destroy coin
            Destroy(gameObject);
        }
    }

    // public GameObject mCollectionAnimationObject;

    // //private StateManager _mStateManager;
    // //private SoundManager _mSoundManager;

    // void Start() {
    //     //_mStateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>();
    //     //_mSoundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    // }

    // void OnCollisionEnter2D(Collision2D other) {
    //     if (other.transform.tag == "Player") {
    //         //_mStateManager.IncrementNbPickups();
    //         //_mSoundManager.NotifyPickup();
    //         Instantiate(mCollectionAnimationObject, transform.position, Quaternion.identity);
    //         Destroy(gameObject);
    //     }
    // }
}