using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    
    public int GetDamage()
    {
        return damage;
        
    }

    public void Hit()
    {
       Debug.Log("Hi");
    } 

     private void OnTriggerEnter2D(Collider2D other)
     {
        Hit();
     }
}
