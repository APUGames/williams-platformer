using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    private void Awake()
    {
        // Find the number of similar objects in the scene
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // If Scene reloads, Singleton won't be destroyed
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            SubtractLife();
        }
        else
        {
            ResetGameSession();
        }
    }

        private void SubtractLife()
        {
            playerLives--;

            Debug.Log("Player Lives: " + playerLives);

            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        private void ResetGameSession()
        {
        SceneManager.LoadScene(0);

        Destroy(gameObject);
        }
}
