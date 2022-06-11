// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Shooting : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public Transform firePoint;
//     public GameObject bulletPrefab;

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && PlayerController.hasGun1) {
            
//             Shoot();
//         }
//     }

//     void Shoot(){
//         Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//     }
// }
