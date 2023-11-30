using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsScript : MonoBehaviour
{
    //Todas las estructuras de datos empieza por el n�mero 0

    public string student1 = "Christian";
    public string student2 = "Kate";
    public string student3 = "Mia";
    public string student4 = "Anastasia";

    /*  ARRAY
     *      - Es hom�geneo (solo un tipo de dato)
     *      - Es de tama�o fijo (una vez creado, no puede contener m�s elementos)
     *      - Tiene un orden (se accede por posici�n)
     */

    public string[] students = new string[] { "Christian", "Kate", "Mia", "Anastasia" };
    public string[] NewFamily = new string[4];//{ , , , }
    private int[] numbersOfDoorsInMyStreet = new int[] { 1, 2, 3, 4, 5, 6 };

    /*  LISTA
     *      - Es hom�geneo (solo un tipo de dato)
     *      - Es de tama�o ajustable o variable(podemos a�adir o eliminar elemntos en tiempo real)
     *      - Tiene un orden (se accede por posici�n)
     */

    public List<string> studentsNames = new List<string>();

    /* ArrayList
     *      - Es heter�geneo (puede guardar diferentes tipos de datos en la misma estructura)
     *      - Es de tama�o ajustable o variable(podemos a�adir o eliminar elemntos en tiempo real)
     *      - Tiene un orden (se accede por posici�n)
     */

    public ArrayList inventory = new ArrayList();

    /*  Diccionario
     *      - Se puede redimensionar din�micamente (igual que una lista)
     *      - Puede contener informaci�n heret�genea (igual que una array list)
     *      - Se accede por clave, no por posici�n
     */

    public Hashtable personalDetails = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        //Add --> a�ade elementos al final de la lista
        //aqu� la lista est� vac�a
        studentsNames.Add("Christian");
        Debug.Log(studentsNames);
        //aqu� la lista tienes 1 elemento, Christian
        studentsNames.Add("Kate");
        Debug.Log(studentsNames);
        //aqu� la lista tienes 2 elemento, Christian y Kate
        studentsNames.Add("Mia");
        Debug.Log(studentsNames);
        //aqu� la lista tienes 3 elemento, Christian, Kate y Mia
        studentsNames.Add("Anastasia");
        Debug.Log(studentsNames);
        //aqu� la lista tienes 4 elemento, Christian, Kate, Mia y Anastasia
        studentsNames.Add("Jack");
        Debug.Log(studentsNames);
        //aqu� la lista tienes 5 elemento, Christian, Kate, Mia, Anastasia y Jack

        //Contains --> nos dice si hay o no un objeto con el nombre o valor que le especifiquemos
        if (studentsNames.Contains("Jack"))
        {
            //Remove --> Elimina elementos de la lista
            studentsNames.Remove("Jack");
        }

        studentsNames.Insert(2, "Paul");
        Debug.Log(studentsNames);
        //ahora el orden es: Christian, Kate, Paul, Mia y Anastasia

        //ToArray() -> Convierte un Lista en una Array
        string[] studentsNamesToArray = studentsNames.ToArray();

        //Clear -> borra definitivamente todos los elementos de la lista
        //studentsNames.Clear();
        //ahora la lista est� vac�a [];
        Debug.Log(studentsNames);

        int pos = 1;
        Debug.Log("El tama�o de la array es: " + students.Length);// Length para contar el tama�o de una array
        if (pos >= 0 && pos < studentsNamesToArray.Length)
        {
            Debug.Log("El segundo estudiante de la Array es: " + students[pos]);
        }
        

        Debug.Log("El tama�o de la lista es: " + studentsNames.Count);//Count-> para contar el tama�o en lista
        string thirdStudents = studentsNames[2];
        Debug.Log("El tercer estudiante de la lista es: " + thirdStudents);



        inventory.Add(39);
        inventory.Add(3.1302);
        inventory.Add("Juan Carlos");
        inventory.Add("false");
        inventory.Add(GameObject.FindGameObjectsWithTag("Fireworks"));

        //Pedimos el tipo de dato que va a salir de la Array list
        Debug.Log(inventory[2].GetType());
        Debug.Log(inventory[4].GetType());

        personalDetails.Add("firstName",    "Juan Gabriel");
        personalDetails.Add("lastname",     "Gomila");
        personalDetails.Add("age",          30);
        personalDetails.Add("gender",       "male");
        personalDetails.Add("isMarried",    false);
        personalDetails.Add("hasChildren",  false);

        if (personalDetails.Contains("firstName"))
        {
            string name = (string)personalDetails["firstName"];
            int age = (int)personalDetails["age"];
        }
        else
        {
            Debug.Log("El diccionario no contiene las claves que se han pedido");
        }
        
        Debug.Log(personalDetails["firstName"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
