using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsPressed : MonoBehaviour
{
    // Start is called before the first frame update
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void PlayAgainAiButton()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
}
