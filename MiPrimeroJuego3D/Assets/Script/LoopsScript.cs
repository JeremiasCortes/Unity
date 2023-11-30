using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(Type elemntName in collection){
         *      //C�digo del bucle
         *      }
         */
        List<string> studentsNames = new List<string>();
        studentsNames.Add("Christian");
        studentsNames.Add("Kate");
        studentsNames.Add("Mia");
        studentsNames.Add("Anastasia");

        Debug.Log("IMPRIMIENDO CON FOREACH");
        foreach(string person in studentsNames)
        {
            Debug.Log(person);
        }

        int[] someInts = new int[] { 4, 8, 3, 0, 9, 6, 8, 7 };
        int sum = 0;
        int n = someInts.Length;
        foreach(int i in someInts)
        {
            sum = sum + i;
            Debug.Log("La suma vale ahora:" + sum);
        }
        Debug.Log("La media de los n�meros vale: " + sum / n);

        /*  FOR -> acceder a posiciones
         *  for(inicializaci�n; condici�n de continuaci�n; iterador){
         *      //C�digo del bucle
         *  }
         */

        Debug.Log("IMPRIMIENDO CON EL FOR");
        for (int i = 1; i <= 10; i++)
        {
            Debug.Log(i);
        }

        Debug.Log("CUENTA ATR�S CON EL FOR");
        for (int j = 10; j >= 0; j--)
        {
            Debug.Log(j);
        }

        for (int pos = 0; pos < studentsNames.Count; pos++)
        {
            Debug.Log("El elemento n�mero " + pos + " de la lista es " + studentsNames[pos]);
        }

        /*  WHILE
         *  Inicializaci�n
         *  while(condici�n de continuaci�n){
         *  //C�digo a ejecutar
         *  iterador
         *  }
         *  La variable del bucle sigue existiendo despu�s...
         */

        Debug.Log("CONTAR DEL 1 AL 10 CON BUCLE WHILE");
        int counter = 1;
        while (counter <= 10)
        {
            Debug.Log(counter);
            counter++;
        }
        



        for(int i = 0; i < 100; i++)
        {
            if (i == 0)
            {
                Debug.Log("El 0 es un n�mero especial...");
            }
            else if (IsNumberEven(i))
            {
                Debug.Log("El numero " + i + " es par.");
            }
            else
            {
                Debug.Log("El numero " + i + " es inpar.");
            }
        }
        
        bool IsNumberEven(int number) // ever = par, odd = impar
            //int quotient = number / 2;
        {
            int remainder = number % 2;//resto de dividir number entre 2
            
            if (remainder== 0)
            {
                return true;
            }
            else //el resto en este caso ser� 1
            {
                return false;
            }
        }

        Debug.Log("N�meros primos");
        for (int number = 2; number < 100; number++)
        {
            bool isPrime = true;
            for (int i = 2; i < number; i++)
            {
                int remainder = number % i;
                if (remainder == 0)
                {
                    isPrime = false;
                }
            }

            if (isPrime)
            {
                Debug.Log("El n�mero " + number + " es primo");
            }
            /*else
            {
                Debug.Log("El n�mero " + number + " es compuesto");
            }*/

            Debug.Log("Algoritmo de b�squeda");
            int objectPos = -1;
            for(int i = 0; i < studentsNames.Count; i++)
            {
                Debug.Log("Vamos por la iteraci�n n�mero 0" + i);
                if (studentsNames[i] == "Juan Gabriel")
                {
                    
                    objectPos = i;
                    break;
                }
            }

            if (objectPos == -1)
            {
                Debug.Log("No hemos encontrado el objeto que buscabas");
            }
            else
            {
                Debug.Log("El objeto que buscas se encuentra en la posici�n " + objectPos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
