#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;

    private Rigidbody _rigidbody;
    
    [SerializeField]
    private float turnSpeed;
    
    private Quaternion rotation = Quaternion.identity;

    private AudioSource _audioSource;

    public Canvas moveCanvas;
    public Joystick Move;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Si se está usando el móvil, este será el movimiento
    #if USING_MOBILE
        moveCanvas.enabled = true;
        
        float horizontal = Move.Horizontal;
        float vertical = Move.Vertical;
        //si detecta que se está tocando la pantalla, se pondrá los siguientes datos
        
        
    #else //En caso de no se esté usando lo de móvil pues se pondrá lo siguiente que es el movimiento de teclado
        moveCanvas.enabled = false;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    
    #endif
        
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput ;
        
        _animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
        
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,
            movement,turnSpeed * Time.fixedDeltaTime, 0);

        rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
