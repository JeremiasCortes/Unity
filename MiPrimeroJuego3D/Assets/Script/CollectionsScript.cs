using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsScript : MonoBehaviour
{
    //Todas las estructuras de datos empieza por el número 0

    public string student1 = "Christian";
    public string student2 = "Kate";
    public string student3 = "Mia";
    public string student4 = "Anastasia";

    /*  ARRAY
     *      - Es homógeneo (solo un tipo de dato)
     *      - Es de tamaño fijo (una vez creado, no puede contener más elementos)
     *      - Tiene un orden (se accede por posición)
     */

    public string[] students = new string[] { "Christian", "Kate", "Mia", "Anastasia" };
    public string[] NewFamily = new string[4];//{ , , , }
    private int[] numbersOfDoorsInMyStreet = new int[] { 1, 2, 3, 4, 5, 6 };

    /*  LISTA
     *      - Es homógeneo (solo un tipo de dato)
     *      - Es de tamaño ajustable o variable(podemos añadir o eliminar elemntos en tiempo real)
     *      - Tiene un orden (se accede por posición)
     */

    public List<string> studentsNames = new List<string>();

    /* ArrayList
     *      - Es heterógeneo (puede guardar diferentes tipos de datos en la misma estructura)
     *      - Es de tamaño ajustable o variable(podemos añadir o eliminar elemntos en tiempo real)
     *      - Tiene un orden (se accede por posición)
     */

    public ArrayList inventory = new ArrayList();

    /*  Diccionario
     *      - Se puede redimensionar dinámicamente (igual que una lista)
     *      - Puede contener información heretógenea (igual que una array list)
     *      - Se accede por clave, no por posición
     */

    public Hashtable personalDetails = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        //Add --> añade elementos al final de la lista
        //aquí la lista está vacía
        studentsNames.Add("Christian");
        Debug.Log(studentsNames);
        //aquí la lista tienes 1 elemento, Christian
        studentsNames.Add("Kate");
        Debug.Log(studentsNames);
        //aquí la lista tienes 2 elemento, Christian y Kate
        studentsNames.Add("Mia");
        Debug.Log(studentsNames);
        //aquí la lista tienes 3 elemento, Christian, Kate y Mia
        studentsNames.Add("Anastasia");
        Debug.Log(studentsNames);
        //aquí la lista tienes 4 elemento, Christian, Kate, Mia y Anastasia
        studentsNames.Add("Jack");
        Debug.Log(studentsNames);
        //aquí la lista tienes 5 elemento, Christian, Kate, Mia, Anastasia y Jack

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
        //ahora la lista está vacía [];
        Debug.Log(studentsNames);

        int pos = 1;
        Debug.Log("El tamaño de la array es: " + students.Length);// Length para contar el tamaño de una array
        if (pos >= 0 && pos < studentsNamesToArray.Length)
        {
            Debug.Log("El segundo estudiante de la Array es: " + students[pos]);
        }
        

        Debug.Log("El tamaño de la lista es: " + studentsNames.Count);//Count-> para contar el tamaño en lista
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
