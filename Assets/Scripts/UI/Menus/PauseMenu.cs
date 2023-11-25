using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{

    public InputActionAsset PauseAction;
    public GameObject pauseScreen;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Application.targetFrameRate = 60;
        PauseAction.FindAction("UI/Cancel").performed += TogglePause;
        pauseScreen.SetActive(false);
        ScreenFader.Instance.FadeIn();
    }

    public async void ReturnToMenu()
    {
        
        var awaitable = SceneManager.LoadSceneAsync("MainMenu");
        ScreenFader.Instance.FadeOut(1);
        await awaitable;
        Time.timeScale = 1;


    }

    public void TogglePause(CallbackContext callback)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);

        if (pauseScreen.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    private void OnDestroy()
    {
        PauseAction.FindAction("UI/Cancel").performed -= TogglePause;
    }

}
