using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int number1;
    public int number2;

    private void Awake()
    {
        Debug.Log("El objeto ha despertadp");
    }

    void Start()
    {
        Debug.Log("El objeto ha arrancado");

        AddTwoNumbers(5, 8);
    
    
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddTwoNumbers();
        }
        Debug.Log("El objeto se está actualizando");
        Debug.Log(Time.time);
    }

    void AddTwoNumbers()
    {
        Debug.Log(number1 + number2);
    }

    void AddTwoNumbers(int firstNumber, int secondNumber)
    {
        Debug.Log(firstNumber + secondNumber);
    }

}
