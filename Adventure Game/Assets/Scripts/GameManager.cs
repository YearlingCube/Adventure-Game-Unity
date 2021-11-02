using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject DeathPanel;
    public GameObject PausePanel;

    public GameObject Player;

    public static int Level;

    private void Update()
    {
        if (Level < SceneManager.GetActiveScene().buildIndex)
        {
        Level = SceneManager.GetActiveScene().buildIndex;
        }
        Debug.Log(Level);
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("NEXT LEVEL!");
        Saving.Save();
    }
    public void Die()
    {
        DeathPanel.SetActive(true);
    }
    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        Player.GetComponent<Bandit>().isPaused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}