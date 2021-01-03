using UnityEngine;

public enum MainMenuScenes
{
	Self = 1,
	Level = 2,
	Info = 3,
}

public class MainMenuManager : MonoBehaviour 
{
	[SerializeField] private AudioClip audioLoop = default;
	[SerializeField] private AudioClip fwdClip = default;

	private const string audioLoopName = "MainMenuLoop";

	void Start()
	{
		AudioManager.Instance.PlayLoop2D(audioLoopName, audioLoop, 0.85f, 0.0f, false);
	}
	public void MenuButtonAction(string sceneName) 
	{
		int scene = (int)MainMenuScenes.Self;
		AudioManager.Instance.PlayOneShoot2D(fwdClip);		
		switch(sceneName)
		{
			case "Level":
				scene = (int)MainMenuScenes.Level;
				AudioManager.Instance.StopLoop(audioLoopName);
				break;
			case "Info":
				scene = (int)MainMenuScenes.Info;
				break;
			default:
				scene = (int)MainMenuScenes.Self;
				break;
		}
		AppManager.Instance.ChangeScene(scene);
	}
}
