using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
 
 public class SnekAtkSpawner2 : MonoBehaviour
 {
     [SerializeField]
     public GameObject bossBullet;
 
     [SerializeField]
     private Transform[] spawnPoints;
 
     public float maxTime = 50;
 
     public float minTime = 20;
 
    

     private float time;

     private float spawnTime;
 
 
     void Awake ()
     {
 
         List<Transform> spawningPointsAsList = new List<Transform> ();
 
         // Get All the children of the object this script is assigned to (EnemyManager) and consider them as spawining points
         foreach (Transform child in transform) {
             spawningPointsAsList.Add (child);
         }
 
         spawnPoints = spawningPointsAsList.ToArray ();
     }
 
 
     void Start ()
     {
         SetRandomTime();
         time = 0;
     }

 
     void SetRandomTime ()
     {
         spawnTime = Random.Range (minTime, maxTime);
     }
 
     void FixedUpdate ()
     {
         //Counts up
         time += Time.deltaTime;
         
         //Check if its the right time to spawn the object
         if (time >= spawnTime) {
            
             Debug.Log ("Spawn" + bossBullet.name);
             Spawn2 ();
             SetRandomTime ();
             time = 0;
         }
         
     }
 
     void Spawn2 ()
     {
         int spawnPointIndex = Random.Range (0, spawnPoints.Length);
         Instantiate (bossBullet, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
     }
 }