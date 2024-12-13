using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Ensure using UnityEngine.SceneManagement

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // If you want this manager to reset each time the scene changes,
        // do not use DontDestroyOnLoad here.
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        UnlockCursor();
    }

    public void GoToCreditScreen()
    {
        SceneManager.LoadScene("CreditScreen");
        UnlockCursor();
    }

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        UnlockCursor();
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
        UnlockCursor();
    }

    public void GoToGameLevel()
    {
        SceneManager.LoadScene("GameLevel");
        LockCursor();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
