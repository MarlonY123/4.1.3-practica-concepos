using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;
    public float speedY = 2f;
    [Header("Limites")]
    public float limiteX = 8f;

    [Header("Vida")]
    public int health = 3;

    [Header("Disparo")] 
    public GameObject prefabDisparo;
    public float disparoSpeed =2f;
    public float timeDisparoDestroy = 2f;

    private float _shootingTimer;
    
    public TextMeshProUGUI texto;

    public Transform weapon1;
    public Transform weapon2;
    private bool isFire = false;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        StartFire();
        setHealth();
    }

    public void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, speedY);
        if (transform.position.x > limiteX)
        {
            transform.position = new Vector2(limiteX, transform.position.y);
        }
        else if(transform.position.x < -limiteX)
        {
            transform.position = new Vector2(-limiteX, transform.position.y);
        }
    }

    public void StartFire()
    {
        if (Input.GetAxis("Fire1") == 1f)
        {
            if (!isFire)
            {
                isFire = true;
                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);
                
                disparoInstance.transform.position = weapon1.position;
                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance,timeDisparoDestroy);
                
                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;
                //Move
                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2,timeDisparoDestroy);
            }
            else
            {
                isFire = false;
            }
        }

        
        

    }

    
    private void OnCollisionEnter2D(Collision2D otherCollision)
    {
        //Debug.Log(otherCollision.gameObject.name);
        if (otherCollision.gameObject.tag == "disparoEnemigo")
        {
            health--;
            Destroy(otherCollision.gameObject);
            
            
        }
    }

    private void setHealth(){
        texto.text = "Vida: " + health;
        if(health<=0){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    

}
