using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Basic Methods
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Level Select Methods
    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(4);
    }

    public void LevelThree()
    {
        SceneManager.LoadScene(6);
    }

    public void LevelFour()
    {
        SceneManager.LoadScene(8);
    }
}