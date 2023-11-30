using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // Necesario para detectar interacuaci�n con la UI

public abstract class TradeCupcakeTower : MonoBehaviour, IPointerClickHandler
{
    //Medidor de az�car para saber cu�ntos puntos para gastar tenemos
    protected static SugarMeterScript sugarMeter;

    //Torreta seleccionada para actualmente para ser mejorada o vendida
    protected static TowerScript currentActiveTower;

    // Start is called before the first frame update
    void Start()
    {
        //Comprobamos si el medidor de az�car ha sido inicializado o no
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

    //Funci�n abstracta que ser� llamada cuando unos de los tres botones se pulse y cada uno implementar� una l�gica diferente...
    public abstract void OnPointerClick(PointerEventData eventData);
}
