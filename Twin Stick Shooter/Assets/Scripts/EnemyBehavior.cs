using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 2;

    public Transform explosion;

    public AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // El enemigo choca con el laser
        if (collision.gameObject.name.Contains("Laser"))
        {
            LaserMovement laser = collision.gameObject.GetComponent<LaserMovement>() as LaserMovement;
            health -= laser.damage;
            Destroy(collision.gameObject);

            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }

        // Si nave enemiga se queda sin vida
        if (health <= 0)
        {
            // Se destruye la nave enemiga
            Destroy (this.gameObject);
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            controller.KillEnemy();
            controller.IncreaseScore(Random.Range(1,7));
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2f);
            }
        }

        // El enemigo choca con el jugador
        if (collision.gameObject.name.Contains("player"))
        {
           
        }
    }
}
