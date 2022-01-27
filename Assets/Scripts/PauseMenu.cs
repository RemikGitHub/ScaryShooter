using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI, settingsMenuUI, audioSettingsMenuUI, controlsSettingsMenuUI;
    public Slider mainVolumeSlider, sfxVolumeSlider, musicVolumeSlider;
    public AudioMixer mainMixer;
    public static bool GameIsPaused = false;
    private float volumeMain, volumeSfx, volumeMusic;
    private void Start()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume", volumeMain);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", volumeSfx);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", volumeMusic);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    //powrot do rozgrywki
    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        audioSettingsMenuUI.SetActive(false);
        controlsSettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    //pauza
    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    //wyjscie do menu
    public void Quit(string menu)
    {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene(menu);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    //wlaczanie menu ustawien
    public void LoadSettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    //wlaczanie menu ustawien dzwieku
    public void LoadAudioSettings()
    {
        settingsMenuUI.SetActive(false);
        audioSettingsMenuUI.SetActive(true);
    }
    //wlaczanie menu ustawien wyswietlania
    public void LoadControlsSettings()
    {
        settingsMenuUI.SetActive(false);
        controlsSettingsMenuUI.SetActive(true);
    }
    //wyjscie do menu pauzy
    public void QuitSettings()
    {
        if (settingsMenuUI.activeSelf)
        {
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        else if (audioSettingsMenuUI.activeSelf)
        {
            audioSettingsMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }
        else if (controlsSettingsMenuUI.activeSelf)
        {
            controlsSettingsMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }
    }
    //ustawienie glosnosci Main
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
