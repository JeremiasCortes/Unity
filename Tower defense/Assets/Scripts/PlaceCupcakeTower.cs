//#if UNITY_IOS || UNITY_ANDROID
//#define USING_MOBILE
//#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCupcakeTower : MonoBehaviour
{
    //Variable para referenciar el Game Manager del juego
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //Obtenemos una referencia al Game Manager de la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Conocer las coordenadas del ratón
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        //La torreta se colocará a 7 unidades delante de la camará, como estaba en -10, la torreta quedará en z=-3 como deberíamos al principio
        float z = 7.0f;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));
/*#if USING_MOBILE
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Obtener el primer toque (índice 0)

            if (touch.phase == TouchPhase.Began && gameManager.isPointerOnAllowedArea())
            {
                //Habilitamos el Script de la torreta para que pueda disparar
                GetComponent<TowerScript>().enabled = true;
                //Le añadimo un collider para evitar que se plante otra torreta encima de la misma
                gameObject.AddComponent<BoxCollider2D>();
                Destroy(this); //Destruimos este script
            }
        }

        
#else*/
//Si el jugador hace click en esa posición, vamor a ver si se puede colocar la torre en dicho punto
        if (Input.GetMouseButtonDown(0) && gameManager.isPointerOnAllowedArea())
        {
            //Habilitamos el Script de la torreta para que pueda disparar
            GetComponent<TowerScript>().enabled = true;
            //Le añadimo un collider para evitar que se plante otra torreta encima de la misma
            gameObject.AddComponent<BoxCollider2D>();
            Destroy(this); //Destruimos este script
        }
//#endif


    }
}
