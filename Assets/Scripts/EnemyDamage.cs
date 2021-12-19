using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
        [SerializeField] int damage = 100;
    
    public int GetDamage()
    {
        return damage;
        
    }

    public void Hit()
    {
        Destroy(gameObject);
    } 

     private void OnTriggerEnter2D(Collider2D other)
     {
        Hit();
     }
}
