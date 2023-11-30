using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Family : MonoBehaviour
{

    public Person father;
    public Person mother;
    public Person son;

    // Start is called before the first frame update
    void Start()
    {
        father = new Person("Anakin", "Skywalker");//instaciar
        //después de instanciar, podemos inicializar las variables
        father.age = 35;
        father.isMale = true;

        mother = new Person();
        mother.firstName = "Padme";
        mother.lastName = "Amidala";
        mother.age = 28;
        mother.isMale = false;

        father.spouse = mother;
        mother.spouse = father;

        son = new Person("Luke", "Skywalker", 8, true);

        son.spouse = null;

        Debug.Log(father.firstName + " y " + mother.firstName + " tienen un hijo llamado " + son.firstName);

        if (mother.IsMarriedWith(father))
        {
            Debug.Log(father.firstName + " y " + mother.firstName + " están casados.");
        }
        else
        {
            Debug.Log(father.firstName + " y " + mother.firstName + " no están casados.");
        }
    }
    void Update()
    {
        
    }
}
