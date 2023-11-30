using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour {
   
    public string firstName = "Luke";
    public string lasName = "Skywalker";
    public string email = "Luker@gmail.com";
    public int age = 32;
    public float height = 1.78f;
    public float weight = 82.5f;

    bool variablebooleana; //true o false
    int variableentera;// ...-3, -2, -1, 0, 1, 2, 3...
    float variabecondecimal;//3.149432, -4.53, 3...
    char variablecharacter;//'a', "b", 'c', "@", '#', " ", ...
    string variablestring;//conjunto de caracteres "blablabal$"


    // Start is called before the first frame update
    void Start()
    {
        float playerHeight = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
