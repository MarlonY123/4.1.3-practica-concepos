using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float horizontalMovement = 0.1f;
    private float offSet = 12f;

    
    [Header("Disparo")] 
    public GameObject prefabDisparo;
    public float disparoSpeed =2f;
    public float shootingInterval = 6f;
    public float timeDisparoDestroy = 2f;
    private float _shootingTimer;
    public Transform weapon1;
    public Transform weapon2;

    private float tiempoDisparoDelay = 10f;

    private bool canShoot = true;

    [Header("Vida")]
    public int health = 15;

    

    public float destroyTimeEnemy = 8f;



    // Start is called before the first frame update
    void Start()
    {
        _shootingTimer = Random.Range (0f, shootingInterval);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        StartFire();
        HorizontalMovement();
        Destroy(gameObject, destroyTimeEnemy);
    }
    
    public void StartFire()
    {
        _shootingTimer -= Time.deltaTime;
        if (_shootingTimer <= 0f && canShoot)
        {
            _shootingTimer = shootingInterval;
           GameObject disparoInstance = Instantiate(prefabDisparo);
           disparoInstance.transform.SetParent(transform.parent);
           disparoInstance.transform.position = weapon1.position;
           
           disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
           Destroy(disparoInstance,timeDisparoDestroy);
                    
           GameObject disparoInstance2 = Instantiate(prefabDisparo);
           disparoInstance2.transform.SetParent(transform.parent);
           disparoInstance2.transform.position = weapon2.position;
           
           disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
           Destroy(disparoInstance2,timeDisparoDestroy);
            
        }
    }
    
    void OnTriggerEnter2D (Collider2D otherCollider) {
        if (otherCollider.tag == "disparoPlayer" || otherCollider.tag == "Player") {
            health--;
            if(otherCollider.tag == "Player"){
                otherCollider.gameObject.GetComponent<PlayerManager>().health--;
            }
            else{
                canShoot = false;
                Destroy(otherCollider.gameObject);
            }
            if(health < 0)
            {
                Destroy(gameObject);
            }
            
            Debug.Log(canShoot);
            if(canShoot == false){
                StartCoroutine(ReiniciarDisparo());
            }
        }
    }

    private void HorizontalMovement()
    {
        if (transform.position.x > offSet || transform.position.x < -offSet) {
            horizontalMovement *= -1;
        }
        transform.position = new Vector2(transform.position.x + horizontalMovement, transform.position.y);

    }

    IEnumerator ReiniciarDisparo()
    {
        // Esperar 3 segundos antes de permitir que el enemigo vuelva a disparar
        yield return new WaitForSeconds(tiempoDisparoDelay);
        Debug.Log("Ya puede disparar");
        canShoot = true;
    }
    
}
