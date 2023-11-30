using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // Necesario para detectar interacuación con la UI

public abstract class TradeCupcakeTower : MonoBehaviour, IPointerClickHandler
{
    //Medidor de azúcar para saber cuántos puntos para gastar tenemos
    protected static SugarMeterScript sugarMeter;

    //Torreta seleccionada para actualmente para ser mejorada o vendida
    protected static TowerScript currentActiveTower;

    // Start is called before the first frame update
    void Start()
    {
        //Comprobamos si el medidor de azúcar ha sido inicializado o no
        if (sugarMeter == null)
        {
            //Sino lo ha sido, lo inicializamos 
            sugarMeter =FindAnyObjectByType<SugarMeterScript>();
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetActiveTower(TowerScript newCupcakeTower)
    {
        currentActiveTower = newCupcakeTower;
    }

    //Función abstracta que será llamada cuando unos de los tres botones se pulse y cada uno implementará una lógica diferente...
    public abstract void OnPointerClick(PointerEventData eventData);
}
