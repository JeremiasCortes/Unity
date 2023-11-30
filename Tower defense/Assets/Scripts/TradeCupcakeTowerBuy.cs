using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowerBuy : TradeCupcakeTower
{
    [Tooltip("Variable para identificar el prefab de la torreta que debamos instanciar con este botón")]
    public GameObject cupcakeTowerPrefab;



    public override void OnPointerClick(PointerEventData eventData)
    {
        //Aquí va el código de cuando hagamos Click

        //Recuperamos el precio de construir una torreta
        int price = cupcakeTowerPrefab.GetComponent<TowerScript>().initialCost;
        //Comprobamos si el jugador tiene suficiente dinero como para comprar esta torreta
        if (price <= sugarMeter.getSugarScore())
        {
            //Aquí tenemos suficiente dinero, así que puedo comprar la torreta
            //Descuento el precio del contador
            sugarMeter.AddSugar(-price);
            //Instanciamos el prefab en pantalla
            GameObject newTower = Instantiate(cupcakeTowerPrefab);
            //El prefab instanciado, lo asignamos a la torreta actual
            currentActiveTower = newTower.GetComponent<TowerScript>();
        }
        
    }

}
