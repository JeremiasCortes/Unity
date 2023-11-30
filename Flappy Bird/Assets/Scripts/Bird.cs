using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Velocidad a la que se mueve")]
    private float speed = 2f;

    [SerializeField]
    [Tooltip("Fuerz a la que se impulsa hacia arriba")]
    private float force = 300f;

    public Button tuBoton; // Asigna el botón desde el Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Velocidad hacia la derecha
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Salto del pájaro
        if (Input.GetKeyDown(KeyCode.Space) || 
            (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began) || 
            Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tuBoton.onClick.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        InterstitialAdExample.Instance.LoadAd();
    }
}
