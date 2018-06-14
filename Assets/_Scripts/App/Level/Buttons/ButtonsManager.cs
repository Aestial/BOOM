using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : Singleton<ButtonsManager> 
{
	[HideInInspector] public int numButtons;
	[HideInInspector] public GameButton[] mButtons;
	[HideInInspector] public GameObject[] mCanvasButtons;

	// UI Creation
	[SerializeField] private GameButtons buttonsData;
	[SerializeField] private GameObject gameButtonPrefab;
	[SerializeField] private RectTransform buttonsPanel;

	private Notifier notifier;

	void Awake () 
	{
		this.notifier = new Notifier();
		this.notifier.Subscribe(StateManager.ON_STATE_ENTER, HandleOnStateEnter);
	}
	
	void Start () 
	{
		// Create UI Buttons
		this.CreateButtons();
	}

	public void ShowSequence(GameButton[] sequence)
	{
		StartCoroutine(this.ShowSequenceCoroutine(sequence));
	}

	private void HandleOnStateEnter (params object[] args)
	{
		GameState state = (GameState)args[0];
		switch(state)
		{
			case GameState.Player:
				this.EnableButtons(true);
				break;
			default:
				this.EnableButtons(false);
				break;
		}
	}	
	
	private IEnumerator ShowSequenceCoroutine(GameButton[] sequence) 
	{
		float waitTime = GameManager.Instance.waitTime;
		yield return new WaitForSeconds(waitTime);
		for (int i = 0; i < sequence.Length; i++)
		{
			int index = this.FindIndexOf(sequence[i].name);
			this.mCanvasButtons[index].GetComponent<Image>().color = sequence[i].activeColor;
			yield return new WaitForSeconds(waitTime);
			this.mCanvasButtons[index].GetComponent<Image>().color = sequence[i].normalColor;
			yield return new WaitForSeconds(waitTime/2);
		}
		StateManager.Instance.State = GameState.Player;
	}

	private void CreateButtons()
	{
		this.mButtons = this.buttonsData.buttons;
		this.numButtons = this.mButtons.Length;
		this.mCanvasButtons = new GameObject[this.numButtons];

		for (int i = 0; i < this.numButtons; i++)
		{
			GameObject newButton = Instantiate(gameButtonPrefab, buttonsPanel);
			newButton.GetComponent<Image>().color = this.mButtons[i].normalColor;
			string name = this.mButtons[i].name;
			newButton.GetComponent<Button>().onClick.AddListener(delegate { PlayerPressButton(name); });
			this.mCanvasButtons[i] = newButton;
		}
		// Posible Unity Test ??
		// Debug.Log(this.mCanvasButtons.Length);
	}

	private void EnableButtons (bool enabled)
	{
		for (int i = 0; i < this.mCanvasButtons.Length; i++)
		{
			// TODO: Change
			this.mCanvasButtons[i].GetComponent<Button>().interactable = enabled;
		}
	}

	private void PlayerPressButton(string name)
	{
		Debug.Log("Pressed Button: " + name);
		GameButton button = this.mButtons[this.FindIndexOf(name)];
		GameManager.Instance.VerifyPlayerInput(button);
	}

	private int FindIndexOf(string name)
	{
		for (int i = 0; i < this.numButtons; i++)
		{
			if (this.mButtons[i].name == name)
				return i;
		}
		return -1;
	}

	private void OnDestroy()
    {
        this.notifier.UnsubcribeAll();
    }
	
}
