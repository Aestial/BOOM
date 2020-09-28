using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Canvas canvas = default;
    public bool isPaused;

    public void PauseGame()
    {
        Time.timeScale = 0;
        if (canvas != null)
        {
            canvas.enabled = true;
        }        
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (canvas != null)
        {
            canvas.enabled = false;
        }        
        isPaused = false;
    }

    void Start()
    {
        // TODO: CHECK
        ResumeGame();
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
            PauseGame();
    }
}
