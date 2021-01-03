using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Canvas canvas = default;
    [SerializeField] private AudioClip pauseClip = default;
    [SerializeField] private AudioClip fwdClip = default;
    public bool isPaused;
    private GameState prevState;

    public void PauseGame()
    {
        AudioManager.Instance.PlayOneShoot2D(pauseClip);
        prevState = StateManager.Instance.State;
        ConfigPause(true);
        StateManager.Instance.State = GameState.Pause;
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlayOneShoot2D(fwdClip);
        ConfigPause(false);
        StateManager.Instance.State = prevState;
    }

    void Start()
    {
        ConfigPause(false);
    }

    public void OnApplicationPause(bool pause)
    {
        // TODO: Check Condition!
        if (pause && (StateManager.Instance.State != GameState.Start 
        && StateManager.Instance.State != GameState.End))
            PauseGame();
    }

    private void ConfigPause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        if (canvas != null)
        {
            canvas.enabled = isPaused;
        }        
        isPaused = isPaused;
    }
}
