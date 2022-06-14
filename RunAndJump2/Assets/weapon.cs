using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public float timePass;
    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        timePass += Time.deltaTime;
        if (timePass > 1000) {
            Shoot();
            timePass = 0;
        }
    }
    void Shoot () {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
