using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range (0,1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range (0,1)] float shootSoundVolume = 0.7f;
    [SerializeField] GameObject deathVFX;




    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringpPeriod = 0.1f;
    [SerializeField] float bombProjectileSpeed = 10f;
    [SerializeField] float bombProjectileFiringpPeriod = 0.1f;



    Coroutine firingCoroutine;


    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    

    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) {return;}
        ProcessHit(damageDealer);
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if(health <= 0)
        {
            FindObjectOfType<Level>().LoadGameOver();
            Destroy(gameObject);
            GameObject Explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position,deathSoundVolume );


        }
    }
    

    private void Fire()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
          firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire"))
        {
            StopCoroutine(firingCoroutine);
        }
        


    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringpPeriod);
        }

    }
    
    IEnumerator FireContinuouslyBomb()
    {
        while (true)
        {
            GameObject laser = Instantiate(bombPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bombProjectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(bombProjectileFiringpPeriod);
        }

    }


    private void Move ()
    {
        var deltaX = CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    }

    public int getHealth()
    {
        return health;
    }

}
