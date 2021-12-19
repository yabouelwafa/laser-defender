using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public void Hit()
    {
        Destroy(gameObject);
    } 

     private void OnTriggerEnter2D(Collider2D other)
     {
        Hit();
     }
}
