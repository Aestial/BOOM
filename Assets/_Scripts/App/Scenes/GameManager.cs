using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSequence
{
	public int index;
	public List <GameButton> sequence;

	public GameSequence() 
	{
		this.index = 0;
		this.sequence = new List<GameButton>();
	}

	public void AddButton(GameButton button) 
	{
		this.index++;
		this.sequence.Add(button);
		int listLenght = this.sequence.ToArray().Length;
		// Debug.Log("List length: " + listLenght);
		// Debug.Log("Index: " + this.index);
		// Debug.Log("Color: " + this.sequence.ToArray()[listLenght - 1].normalColor);
	}
}

public class GameManager : MonoBehaviour 
{
	[SerializeField] private int startLength;

	[SerializeField] private GameButtons buttonsData;
	[SerializeField] private GameObject gameButtonPrefab;
	[SerializeField] private RectTransform buttonsPanel;

	[SerializeField] private float waitTime;

	private GameButton[] mButtons;
	private GameObject[] mCanvasButtons;
	private int mNumButtons;

	private GameSequence mPlayerSequence;
	private GameSequence mEnemySequence;

	// Use this for initialization
	void Start () 
	{
		// Create interface
		this.CreateButtons();

		// Sequences
		this.mPlayerSequence = new GameSequence();
		this.mEnemySequence = new GameSequence();
		this.CreateStartSequence();

		StartCoroutine(this.ShowSequence());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void CreateStartSequence() 
	{
		for (int i = 0; i < this.startLength; i++)
		{
			int randomIndex = Random.Range(0, this.mNumButtons);
			GameButton button = this.mButtons[randomIndex];
			button.id = randomIndex;
			this.mEnemySequence.AddButton(button);
		}
	}

	private IEnumerator ShowSequence()
	{
		GameButton[] sequenceArray = this.mEnemySequence.sequence.ToArray();
		yield return new WaitForSeconds(waitTime);
		for (int i = 0; i < sequenceArray.Length; i++)
		{
			this.mCanvasButtons[sequenceArray[i].id].GetComponent<Image>().color = sequenceArray[i].activeColor;
			yield return new WaitForSeconds(waitTime);
			this.mCanvasButtons[sequenceArray[i].id].GetComponent<Image>().color = sequenceArray[i].normalColor;
			yield return new WaitForSeconds(waitTime/2);
		}
	}

	private void CreateButtons()
	{
		this.mButtons = this.buttonsData.buttons;
		this.mNumButtons = this.mButtons.Length;
		this.mCanvasButtons = new GameObject[this.mNumButtons];

		for (int i = 0; i < this.mNumButtons; i++)
		{
			GameObject newButton = Instantiate(gameButtonPrefab, buttonsPanel);
			newButton.GetComponent<Image>().color = this.mButtons[i].normalColor;
			newButton.GetComponent<Button>().onClick.AddListener(delegate { PlayerPressButton(i); });
			this.mCanvasButtons[i] = newButton;
		}

		Debug.Log(this.mCanvasButtons.Length);
	}

	public void PlayerPressButton(int id)
	{
		Debug.Log(id);
	}

}
