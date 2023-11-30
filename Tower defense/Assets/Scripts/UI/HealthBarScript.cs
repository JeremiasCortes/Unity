using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [Tooltip("Vida máxima que tendrá el jugador")]
    public int maxHealth = 100;
    [Tooltip("Referencia al health bar filling de la UI de Unity")]
    private Image fillingImage;
    [Tooltip("Vida actual del jugador")]
    private int _currentHealth;
    public int currentHealth
    {
        get => _currentHealth; //se establece con el valor privado
        set => _currentHealth = value; //le establece al valor privado lo que se modifique en el público
    }

    // Start is called before the first frame update
    void Start()
    {
        fillingImage = GetComponent<Image>();
        
        //Al inicia la partida el nivel máximo es el tope de vida
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //El método aplica daño al jugador y devuelve el estado de Game Over (true)
    public bool ApplyDamage(int damage)
    {
        //Aplicar el daño a la vida actual
        currentHealth -= damage;
        //Si aun me queda vida, debo de actualizar la barra de vida actual
        if (currentHealth>0)
        {
            UpdateHealthBar();
            return false;
        }
        
        //Si llego a esta línea de código es que no me queda vida
        currentHealth = 0;
        UpdateHealthBar();
        return true;
    }
    
    public void updateHealthBar()
    {
        UpdateHealthBar();
    }

     void UpdateHealthBar()
    {
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        //Cálculo el procentaje de vida que me queda (da un valor entre 0 y 1)
        float percentage = currentHealth * 1.0f / maxHealth;
        //Aplico el porcentaje de relleno a la barra de vida
        fillingImage.fillAmount = percentage;
    }
}
