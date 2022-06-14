using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
 
 public class SnekAtkSpawner : MonoBehaviour
 {
     [SerializeField]
     public GameObject bossBullet;
 
     [SerializeField]
     private Transform[] spawnPoints;
 
     public float maxTime = 30;
 
     public float minTime = 10;
 
    [SerializeField]
    public Sprite ogSprite;
    public Sprite newSprite;

     private float time;

    private SpriteRenderer _mspriteRenderer;
  
     [SerializeField]
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
        _mspriteRenderer = gameObject.GetComponent<SpriteRenderer>();
         SetRandomTime();
         time = 0;
     }

     void ChangeSprite()
    {
        _mspriteRenderer.sprite = newSprite; 

    }
    void ChangeSpriteBack()
    {
        _mspriteRenderer.sprite = ogSprite; 

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
             ChangeSprite();
             StartCoroutine("ExecuteAfterTime");
             Debug.Log ("Spawn" + bossBullet.name);
             Spawn ();
             SetRandomTime ();
             time = 0;
         }
         
     }

    IEnumerator ExecuteAfterTime()
    {
     yield return new WaitForSeconds(3.0f);
 
        ChangeSpriteBack();
    }
 
 
     void Spawn ()
     {
         int spawnPointIndex = Random.Range (0, spawnPoints.Length);
         Instantiate (bossBullet, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
     }
 }