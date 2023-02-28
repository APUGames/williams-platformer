using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Public instead of Private because the function needs to be accessible by the button object

    public void StartGame()
    {
        SceneManager.LoadScene(1); // The Scene after Scene 0 (Menu)
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(1); // 
    }
}
