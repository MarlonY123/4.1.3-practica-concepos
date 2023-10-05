using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    [Header("Enemigos")]
    //public GameObject globoPrefab;
    public GameObject[] enemigos;
    public int spawnCount = 10;
    public int spawnOffset = 10;

    [Header("Spawn Time")]
    private float startDelay = 0f;
    public float repeatTime = 2.5f;
    public GameObject targetCamera;
    private int verticalOffset = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (enemigos.Length != 0)
        {
            //Spawn
            InvokeRepeating("SpawnEnemigos", startDelay, repeatTime);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("No tiene enemigos asignados");
#endif
        }
    }

    private void FixedUpdate()
    {
        if (targetCamera != null)
        {
            transform.position = new Vector2(0, targetCamera.transform.position.y + verticalOffset);

        }
    }

    void SpawnEnemigos()
    {
        for (int i = 0; i < spawnCount; i++)
        {


            //Posicion
            float xPosition = GetRandomPositionX();
            Vector2 spawnPosition = new Vector2(xPosition, transform.position.y);
            //Instancias

            //Instantiate(globoPrefab, spawnPosition, Quaternion.identity);
            //Arreglo de globos
            Instantiate(enemigos[GetRandomEnemigo()], spawnPosition, Quaternion.identity);

        }
    }

    int GetRandomEnemigo()
    {
        int randomEnemigo = Random.Range(0, enemigos.Length);
        return randomEnemigo;
    }

    int GetRandomPositionX()
    {
        int randomPosX = Random.Range(-spawnOffset, spawnOffset);
        
        return randomPosX;

    }


}
