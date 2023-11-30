using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockGenerator : MonoBehaviour
{
    public LevelBlock levelBlock; //Bloque del nivel que hay que generar
    public LevelBlock lastedLevelBlock; //Último bloque colocado    

    public LevelBlock currentLevelBlock; //El que hemos generado ahora mismo

    public PipeMovement blockPipe;
    [SerializeField]
    [Tooltip("Tiempo de generar un bloque de pipe")]
    private float blockGeneratedTime = 2f;

    private void Start()
    {
        AddNewBlock();

        InvokeRepeating("GenerateNewBlockPipe", 0, blockGeneratedTime);
    }

    private void Update()
    {
        float xCam = Camera.main.transform.position.x;
        float xOld = lastedLevelBlock.exitPoint.position.x;

        if (xCam > xOld + lastedLevelBlock.width / 2)
        {
            RemoveOldBlock();
        }
    }

    public void AddNewBlock()
    {
        LevelBlock block = (LevelBlock) Instantiate(levelBlock);
        block.transform.SetParent(this.transform, false);

        Vector3 blockPosition = Vector3.zero;
        blockPosition = new Vector3(lastedLevelBlock.exitPoint.position.x + block.width/2, 
                                    lastedLevelBlock.exitPoint.position.y,
                                    lastedLevelBlock.exitPoint.position.z);

        block.transform.position = blockPosition;

        currentLevelBlock = block;
    }

    public void RemoveOldBlock()
    {
        lastedLevelBlock.transform.position =new Vector3(lastedLevelBlock.transform.position.x+2 * lastedLevelBlock.width,
                                                         lastedLevelBlock.transform.position.y,
                                                         lastedLevelBlock.transform.position.z);

        LevelBlock temp = lastedLevelBlock;
        lastedLevelBlock = currentLevelBlock;
        currentLevelBlock = temp;

        //AddNewBlock();
    }

    public void GenerateNewBlockPipe()
    {
        float distanceToGenerate = levelBlock.width / 2;

        float randomY = Random.Range(-2, 4);

        float randomVelocity = Random.Range(0, 6);

        PipeMovement pipeblock = (PipeMovement) Instantiate(blockPipe);
        Vector3 pipePosition = Vector3.zero;
        pipePosition = new Vector3(Camera.main.transform.position.x + distanceToGenerate,
                                   0, 0);

        pipeblock.speed = randomVelocity;
        pipeblock.transform.position = pipePosition;
        
    }
}
