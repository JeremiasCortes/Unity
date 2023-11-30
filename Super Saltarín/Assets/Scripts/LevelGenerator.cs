using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;

    public LevelBlock firstBlock;

    //nivel_1, nivel_2, nivel_3, nivel_4...
    [Tooltip("Lista con los niveles disponibles para jugar")]
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    // nivel inicial
    public Transform levelStartPoint;

    [Tooltip("Lista con todos los bloques actuales en la escena")]
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();


    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        //Generamos el bloque inicial
        GenerateInitialBlocks();
    }

    /// <summary>
    /// Función para ir añadiendo los bloque de nivel
    /// </summary>
    public void AddLevelBlock()
    {
        // Genera un número aleatorio entre a y b
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        
        LevelBlock currentBlock;

        

        Vector3 spawnPosition = Vector3.zero;

        if (currentBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoint.position;
        } else
        {
            // Instanciamos una copia del prefab bloque de nivel en pantalla
            currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
            // Pone el nuevo bloque de nivel instanciado como hijo del Level Generator
            currentBlock.transform.SetParent(this.transform, false);

            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(spawnPosition.x-currentBlock.startPoint.position.x,
                                         spawnPosition.y-currentBlock.startPoint.position.y,
                                         0);

        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);
    }


    /// <summary>
    /// Función para ir quitando los bloques de nivel antiguos
    /// </summary>
    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldesBlock = currentBlocks[0];
        currentBlocks.Remove(oldesBlock);
        Destroy(oldesBlock.gameObject);
    }

    /// <summary>
    /// Función para quitar todos los bloques del nivel
    /// </summary>
    public void RemoveAllBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }

    /// <summary>
    /// Función para generar el bloque de nivel inicial
    /// </summary>
    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 3; i++){
            AddLevelBlock();
        }
    }
}
