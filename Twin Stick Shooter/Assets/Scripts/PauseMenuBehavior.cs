using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuBehavior : MainMenuBehavior
{
    public static bool isPaused = false;

    public GameObject pauseMenu;

    public GameObject optionsMenu;

    void Start()
    {
        ContinueGame();
        UpdateQualityLabel();
        UpdateVolumeLabel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            if (!optionsMenu.activeInHierarchy)
            {
                isPaused = true;
                pauseMenu.SetActive(isPaused);
                Time.timeScale = 0;

                pauseMenu.SetActive(isPaused);
            }
            else
            {
                OpenPauseMenu();
            }
            
        }
    }

    public void ContinueGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        ContinueGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityLabel();
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityLabel();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        UpdateVolumeLabel();
    }

    private void UpdateQualityLabel()
    {
        int currentQuality = QualitySettings.GetQualityLevel();
        string qualityName = QualitySettings.names[currentQuality];

        optionsMenu.transform.Find("QualityLabel").GetComponent<TextMeshProUGUI>().text = "Calidad igual: " + qualityName;
    }

    private void UpdateVolumeLabel()
    {
        float audioVolume = AudioListener.volume * 100;

        optionsMenu.transform.Find("MasterVolume").GetComponent<TextMeshProUGUI>().text = "Nivel de volumen: " + audioVolume.ToString("f2")+"%";
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
