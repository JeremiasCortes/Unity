using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [Header("Variables de ataque")]
    [Tooltip("Distancia máxima a la que puede disparar la torreta")]
    public float rangeRadius;
    
    [Tooltip("Tiempo de recharda antes de poder disparar otra vez")]
    public float reloadTime;
    
    [Tooltip("Prefab del proyectil que dispara la torreta")]
    public GameObject projectil;
    
    [Tooltip("Tiempo que ha pasado desde la última ve que la torreta disparó")]
    private float timeSinceLastShot;

    [Tooltip("Tipo de torreta")]
    public string tipoDeTorreta;
    public bool elementoDesbloqueado = false;
    
    [Header("Variable de torretas")] [Tooltip("Nivel actual de la torreta")]
    private int _upgradeLevel = 0;
    public int upgradeLevel
    {
        get
        {
            return _upgradeLevel;
        }
        set
        {
            _upgradeLevel = value;
        }
    }
    
    [Tooltip("Sprietes de los diferentes niveles de la torreta")]
    public Sprite[] upgradeSprites;

    [Tooltip("Variable para saber si una torreta se puede upgradear")]
    public bool isUpgradeable = true;

    [Header("Economía de la torreta")]
    [Tooltip("Precio de comprar de la torreta")]
    public int initialCost;
    [Tooltip("Precio de mejorar la torreta de lvl")]
    public int upgradeCost;
    [Tooltip("Precio de venta de la torreta")]
    public int sellCost;
    [Tooltip("Precio de incremento de mejora")]
    public int upgradeIncrementCost;
    [Tooltip("Precio de incremento de venta")]
    public int sellIncrementCost;

    [Tooltip("Game Objects de los proyectiles")]
    public GameObject[] projectilePrefab;

    public PandaScript pandaScript;

    private void Start()
    {
        pandaScript = FindObjectOfType<PandaScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastShot >= reloadTime)
        {
            
            //Encontrar todos los GameObjects que contengan collider dentro de mi rango de disparo
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, rangeRadius);

            if (hitColliders.Length!=0)
            {
                //Programar la lógica de disparo contra todos los posibles objetivos
                //Bucle entre todos los objetivos anteriores para encontrar el panda más cercano
                float minDistance = int.MaxValue;
                int index = -1;

                
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "Enemy")
                    {
                        //Estoy seguro que he chocado contra un panda
                        float Distance = Vector2.Distance(hitColliders[i].transform.position, this.transform.position);
                        if (Distance < minDistance)
                        {
                            index = i;
                            minDistance = Distance;
                        }
                    }
                }

                if (index<0)
                {
                    return;
                }
             //Si estamos aquí, es que tenemos un objetivo al que disparar!

;

             Transform target = hitColliders[index].transform;

             Vector2 direction = (target.position - this.transform.position).normalized;
             
             //Creamos el proyectil haciendo una instancia del prefab que tenemos
             GameObject projectile =
                 GameObject.Instantiate(projectil, this.transform.position, Quaternion.identity) as GameObject;
             projectile.GetComponent<ProjectilesScript>().direction = direction;

                //Código para disparar
                timeSinceLastShot = 0;
            }
        }

        timeSinceLastShot += Time.deltaTime;
    }

    /// <summary>
    /// Método para subir de nivel una torreta
    /// </summary>
    public void UpgradeTower()
    {
        //Chequeamos si podemos subir de nivel la torreta
        if (!isUpgradeable)
        {
            return;
        }
        
        //Si estamos aquí es porque se puede subir de nivel la torreta
        this.upgradeLevel++;

        if (this._upgradeLevel == upgradeSprites.Length - 1)
        {
            isUpgradeable = false;
            elementoDesbloqueado = true;
        }
        
        //Mejorar estados de la torreta;
        rangeRadius += 2.5f;
        reloadTime -= 0.625f;

        //Subimos los precios de mejora y venta
        upgradeCost += upgradeIncrementCost;
        sellCost += sellIncrementCost;

        this.GetComponent<SpriteRenderer>().sprite = upgradeSprites[_upgradeLevel];
        this.projectil = projectilePrefab[upgradeLevel];
    }

    //Este método será llamada cuando el usuario haga click sobre una de las torretas
    private void OnMouseDown()
    {
        //Cuando el usuario clique en una torreta, esta se convierte en la torreta activa actual
        TradeCupcakeTower.SetActiveTower(this);
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
    }
}
