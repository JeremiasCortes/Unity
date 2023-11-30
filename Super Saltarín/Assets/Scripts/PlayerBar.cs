using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Dos posibles tipos de barra
public enum BarType
{
    health,
    mana
}
public class PlayerBar : MonoBehaviour
{
    Slider slider;

    public BarType type;


    private void Start()
    {
        this.slider = GetComponent<Slider>();

        // Dependiendo del tipo de barra
        switch (this.type)
        {
            // Si es el de vida
            case BarType.health:
                // Establecer con su respectiva cantidad inicial
                this.slider.value = PlayerController.INITIAL_HEALTH;
                break;
            // Si es el de mana
            case BarType.mana:
                // Establecer con su respectiva cantidad inicial
                this.slider.value = PlayerController.INITIAL_MANA;
                break;
        }
    }

    private void Update()
    {
        // Dependiendo del tipo de barra
        switch (this.type)
        {
            // Si es el de vida
            case BarType.health:
                // Establece gráficamente el valor de vida
                this.slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            // Si es el de mana
            case BarType.mana:
                // Establece gráficamente el valor de mana
                this.slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }
}
