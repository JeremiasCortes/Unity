using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        //SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
