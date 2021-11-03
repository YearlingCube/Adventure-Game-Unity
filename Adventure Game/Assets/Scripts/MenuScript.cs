using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    void Awake()
    {
        Saving.Load();
        if (GameManager.Level <= Saving.SavedLevel)
        {
        GameManager.Level = Saving.SavedLevel;
        }
    }
    // Play Button
    public void Play()
    {
        // In The Future We Will Add A Level Menu For Selecting Which Level You Wanna Play
        if (GameManager.Level <= 2)
        {
            GameManager.Level = 3;
        }
        SceneManager.LoadScene(GameManager.Level);
    }
    // Settings Button
    public void Settings()
    {
        // Opens Settings Menu
        SceneManager.LoadScene("SettingsMenu");
    }
    // Quit Button
    public void Quit()
    {
        Saving.Save();
        // Quits Application
        Saving.Save();
        Debug.Log("QUIT!");
        Application.Quit();
    }
    // Opens Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Opens Level Selection Menu
    public void LevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
    public void ResetData()
    {
        Saving.ResetLevel();
        Debug.Log(Saving.SavedLevel);
    }
    public static void ResetLevel()
    {
        LevelSelectionScript.Reset = true;
    }
}