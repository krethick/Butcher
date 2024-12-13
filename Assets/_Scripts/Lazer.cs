using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject Laser;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0))
       {
         Shoot();
       } 

       if(Input.GetMouseButtonUp(1))
       {
         StopShooting();
       }
    }

    void Shoot()
    {
        Laser.SetActive(true);
    }

    void StopShooting()
    {
        Laser.SetActive(false);
    }
}
