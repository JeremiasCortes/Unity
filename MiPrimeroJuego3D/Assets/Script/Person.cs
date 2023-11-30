using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{

    public string firstName;
    public string lastName;
    public int age;
    public bool isMale;

    public Person spouse;

    public Person()//COnstructor de por defecto
    {

    }

    public Person(string pFirstName, string pLastName)
    {
        this.firstName = pFirstName;
        this.lastName = pLastName;
    }

    public Person(string firstName, string lastName, int age, bool isMale)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.isMale = isMale;
    }

    public bool IsMarriedWith(Person otherPerson)
    {
        if(spouse == null)
        {
            Debug.Log("No est� casado");
            return false;
            //aqu� no est� casado
        }
        else
        {
            Debug.Log("Est� casado");
            if (otherPerson.firstName == this.spouse.firstName && otherPerson.lastName == this.spouse.lastName)
            {
                Debug.Log("Est� casado con la otra persona");
                return true; 
                //aqu� si est� casdo con otherPerson
            }
            else
            {
                Debug.Log("Est�n casado pero con la otra persona");
                return false; 
                //aqu� est� casado pero no con otherPerson
            }
        }
            
      
    }
    
}
