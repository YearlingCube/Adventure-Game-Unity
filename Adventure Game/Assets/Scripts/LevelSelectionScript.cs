using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class LevelSelectionScript : MonoBehaviour
{
    public GameObject Level1Button;
    public GameObject Level2Button;
    public GameObject Level3Button;
    public GameObject Level4Button;
    // Level Unlocked States
    public Sprite Level2ButtonUnlockedState;
    public Sprite Level3ButtonUnlockedState;
    public Sprite Level4ButtonUnlockedState;
    // Level Locked States
    public Sprite Level2ButtonLockedState;
    public Sprite Level3ButtonLockedState;
    public Sprite Level4ButtonLockedState;

    int level = GameManager.Level;
    void Update()
    {
        Debug.Log(level);
        // Check if Level is Locked
        if (Level2Button.GetComponent<Image>().sprite = Level2ButtonLockedState)
        {
            if (level >= 4)
            {
                Level2Button.GetComponent<Image>().sprite = Level2ButtonUnlockedState;
            }
        }
        if (Level3Button.GetComponent<Image>().sprite = Level3ButtonLockedState)
        {
            if (level >= 5)
            {
                Level3Button.GetComponent<Image>().sprite = Level3ButtonUnlockedState;
            }
        }
        if (Level4Button.GetComponent<Image>().sprite = Level4ButtonLockedState)
        {
            if (level >= 6)
            {
                Level4Button.GetComponent<Image>().sprite = Level4ButtonUnlockedState;
            }
        }
    }
    // Loads Level 1
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    // Loads Level 2
    public void Level2()
    {
        if (Level2Button.GetComponent<Image>().sprite = Level2ButtonUnlockedState)
        {
        SceneManager.LoadScene("Level2");
        }

    }
    // Loads Level 3
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
    // Loads Level 4
    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }
}