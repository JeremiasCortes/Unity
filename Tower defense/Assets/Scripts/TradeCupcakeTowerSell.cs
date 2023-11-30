using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TradeCupcakeTowerSell : TradeCupcakeTower
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //Aquí irá el código de cuando queramos vender una torreta
        //Comprobaré si hay una torreta seleccionada para ser vendida
        if (currentActiveTower == null)
        {
            return;
        }
        //Si llego aquí es que tengo una torreta seleccionada
        //Consulto el precio de venta de la torreta
        int sellingPrince = currentActiveTower.sellCost;
        //Sumamos el dinero al medidor de azúcar del usuario
        sugarMeter.AddSugar(sellingPrince);
        currentActiveTower.DestroyTower();
    }
}
