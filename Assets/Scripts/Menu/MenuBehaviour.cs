using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Orbits");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SelectOptions()
    {
    }

    public void SelectAuthors()
    {
    }
}
