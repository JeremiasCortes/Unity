using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 4f;
    private float currentSpeed = 0f;
    private Vector3 lastMovement = new Vector3 ();

    public Transform laser;
    public float laserDistance = 0.5f;
    public float timeBetweenFires = 0.5f;
    private float timeUnitNextFire = 0f;

    public List<KeyCode> shootButton;

    public AudioClip shootSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehavior.isPaused)
        {
            Rotate();
            Move();
            Fire();
        }
    }


    void Rotate()
    {
        Vector3 worldPosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(worldPosition);

        Vector3 spaceShipPos = this.transform.position;
        float dX = spaceShipPos.x -worldPosition.x;
        float dY = spaceShipPos.y -worldPosition.y;

        float angle = Mathf.Atan2(dY, dX) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle+90));

        this.transform.rotation = rot;
    }

    void Move()
    {
        Vector3 movement = new Vector3();

        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        movement.Normalize();

        if (movement.magnitude > 0)
        {
            // Si el usuario realmente está pulsando las teclas de movimeinto
            currentSpeed = playerSpeed;

            this.transform.Translate(movement * Time.deltaTime * currentSpeed, Space.World);

            lastMovement = movement;
        } else
        {
            // Seguir con la inercia del último movimiento
            this.transform.Translate(movement * Time.deltaTime * currentSpeed, Space.World);
            currentSpeed *= 0.9f;
        }
    }

    void Fire()
    {
        foreach(KeyCode key in shootButton)
        {
            if(Input.GetKeyDown(key) && timeUnitNextFire < 0)
            {
                timeUnitNextFire = timeBetweenFires;
                ShootLaser();
                break;
            }
        }
        timeUnitNextFire -= Time.deltaTime;
    }

    void ShootLaser()
    {
        audioSource.PlayOneShot(shootSound);

        Vector3 laserPos = this.transform.position; // Posición actual de la nave

        float rotationAngle = this.transform.localEulerAngles.z - 90; //grados

        laserPos.x += -Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * laserDistance;
        laserPos.y += -Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * laserDistance;

        Instantiate(laser, laserPos, this.transform.rotation);
    }
}
