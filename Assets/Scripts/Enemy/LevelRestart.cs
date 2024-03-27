using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    // Define the key to restart the level
    public KeyCode restartKey = KeyCode.R;

    // Update is called once per frame
    void Update()
    {
        // Check if the restart key is pressed
        if (Input.GetKeyDown(restartKey))
        {
            // Restart the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}