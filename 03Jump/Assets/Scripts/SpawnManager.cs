using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; //Creamos una Array
    // private Vector3 spawnPos = new Vector3 (30, 0, 0);
    private Vector3 spawnPosition; //Creamos una variable de 3 coordenadas
    private float stardDelay = 2f;
    
    
    public float repeatRate = 1f;
    

    private PlayerController _playerController; //Llamamos a un script

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); //Buscamos el componente PlayerController en el GameObject Player y lo instaciamos en una variable

        spawnPosition = this.transform.position; //Guardamos la posición de donde se encuentra el objeto con este script (30,0,0)
        InvokeRepeating("spawnObstacle", stardDelay, repeatRate); //Invocamos repetidamente con su tiempo de espera del primer objeto y el tiempo que hay entre invocación
        
    }
    
    
    void spawnObstacle()
    {
        if (!_playerController.GameOver) //Si no hay game over
        {
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]; //Dentro de la variable tendrá un valor random de indice de la array desde 0 hasta el máximo tamaño de la Array
        Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation); //INstanciaremos la variable en la posición y con su rotación
        }
    }
}
