using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TradeCupcakeTowerUpgrade : TradeCupcakeTower
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //Este método se ejecutará cuando querramos subir de nivle una torreta
        //Comprobamos si hay una torreta seleccionada para subirla de nivel
        if (currentActiveTower == null)
        {
            return;
        }
        //Si estamos aquí, hay una torreta seleccionada
        //Solo podemos subirla de nivel sino está ya al máximo y si tenemos el suficiente dinero
        int upgradePrice = currentActiveTower.upgradeCost;
        if (currentActiveTower.isUpgradeable &&  upgradePrice <= sugarMeter.getSugarScore()) 
        {
            //Le descontamos el dinero al medido de azúcar del jugador
            sugarMeter.AddSugar(-upgradePrice);
            //Subo el nivel de la torreta actual
            currentActiveTower.UpgradeTower();
        }
    }
}
