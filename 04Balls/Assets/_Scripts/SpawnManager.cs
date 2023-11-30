using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float spawnRange = 9;

    public int enemyCount;
    private int enemyWave = 1;

    public GameObject powerUpPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        //Llamamos a la función que instanciará las oleadas enemigas
        SpawnEnemyWave(enemyWave);
    }

    void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Genera una posición aleatoria dentro de la zona de juego
    /// </summary>
    /// <returns>Devuelve un posición aleatoria dentro de la zona de juego</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return randomPosition;
    }
    
    /// <summary>
    /// Metodo para ir generando un número determinado de enemigos
    /// <param name="numberOfEnemies">Números de enemigos a crear</param>
    /// </summary>
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        
        for (int i = 0; i < numberOfEnemies; i++) //Instanciamos 0, 1 y 2. Un total de 3 veces
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
