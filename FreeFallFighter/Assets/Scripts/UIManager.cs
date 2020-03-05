using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("In-Game UI Layout");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu UI Layout");
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene("Character Selection");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
