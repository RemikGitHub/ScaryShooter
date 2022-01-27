using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI, settingsMenuUI, audioSettingsMenuUI, displaySettingsMenuUI, controlsPanelUI;
    public Slider mainVolumeSlider, sfxVolumeSlider, musicVolumeSlider;
    public AudioMixer mainMixer;
    public Dropdown resolutionDropdown;
    private float volumeMain, volumeSfx, volumeMusic;

    Resolution[] resolutions;

    //pobieranie rozdzielczości komputera
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        mainVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume", volumeMain);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", volumeSfx);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", volumeMusic);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitSettings();
        }
    }
    public void NewGame()
    {
        mainMenuUI.SetActive(false);
        controlsPanelUI.SetActive(true);
    }
    //ladowanie pierwszego poziomu
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    //wychodzenie z gry
    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
		        Application.Quit();
        #endif
    }
    //wlaczanie menu ustawien
    public void LoadSettingsMenu()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    //wlaczanie menu ustawien dzwieku
    public void LoadAudioSettings()
    {
        settingsMenuUI.SetActive(false);
        audioSettingsMenuUI.SetActive(true);
    }
    //wlaczanie menu ustawien wyswietlania
    public void LoadDisplaySettings()
    {
        settingsMenuUI.SetActive(false);
        displaySettingsMenuUI.SetActive(true);
    }
    //wychodzenie z ustawien do poprzedniego menu
    public void QuitSettings()
    {
        if(settingsMenuUI.activeSelf)
        {
            settingsMenuUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }
        else if(audioSettingsMenuUI.activeSelf)
        {
            audioSettingsMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }
        else if(displaySettingsMenuUI.activeSelf)
        {
            displaySettingsMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }
        else if(controlsPanelUI.activeSelf)
        {
            mainMenuUI.SetActive(true);
            controlsPanelUI.SetActive(false);
        }
    }
    //Ustawienie rozdzielczości ekranu
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetMainVolume(float mainVolume)
    {
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
        mainMixer.SetFloat("mainVolume", mainVolume);
    }
    //ustawienie glosnosci SFX
    public void SetSFXVolume(float sfxVolume)
    {
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        mainMixer.SetFloat("sfxVolume", sfxVolume);
    }
    //ustawienie glosnosci Music
    public void SetMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        mainMixer.SetFloat("musicVolume", musicVolume);
    }
}