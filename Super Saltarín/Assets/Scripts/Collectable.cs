using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money,
    points
}
public class Collectable : MonoBehaviour
{

    public CollectableType type = CollectableType.points;
    
    //variable para saber si la moneda ha sido recogida o no
    bool isCollected = false;

    public int valueScore = 0;

    public int value = 0;

    public AudioClip collectSound;



    /// <summary>
    /// Método para activar la moneda y su collider
    /// </summary>
    public void Show()
    {
        //activamos los sprites de los coleccionable -> de rebote también la animación
        this.GetComponent<SpriteRenderer>().enabled = true;
        //activamos los colliders
        switch (this.type)
        {
            case CollectableType.money:
                this.GetComponent<CircleCollider2D>().enabled = true;
                break;
            case CollectableType.points:
                this.GetComponent<CircleCollider2D>().enabled = true; 
                break;
            case CollectableType.healthPotion:
                this.GetComponent<CapsuleCollider2D>().enabled = true;
                break;
            case CollectableType.manaPotion:
                this.GetComponent<CapsuleCollider2D>().enabled = true;
                break;
        }
        isCollected = false;
    }

  
    /// <summary>
    /// Método para desactivar la moneda y su collider
    /// </summary>
    void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        switch (this.type)
        {
            case CollectableType.money:
                this.GetComponent<CircleCollider2D>().enabled = false;
                break;
            case CollectableType.points:
                this.GetComponent<CircleCollider2D>().enabled = false;
                break;
            case CollectableType.healthPotion:
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                break;
            case CollectableType.manaPotion:
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                break;
        }
        
    }


    /// <summary>
    /// Método para recolectar la moneda
    /// </summary>
    void Collect()
    {
        isCollected = true;
        Hide();

        AudioSource audio = GetComponent<AudioSource>();

        if (audio != null && this.collectSound != null)
        {
            audio.PlayOneShot(this.collectSound);
        }


        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.AddPointsScore(valueScore);
                GameManager.sharedInstance.CollectObject(value);
                GetComponent<AudioSource>().PlayOneShot(this.collectSound);
                
                break;
            case CollectableType.points:
                GameManager.sharedInstance.AddPointsScore(valueScore);
                break;
            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectMana(value);
                break;
            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            Collect();
        }
    }
}
