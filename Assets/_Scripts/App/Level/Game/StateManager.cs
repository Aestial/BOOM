using UnityEngine;
public enum GameState
{
    /** Game Start */
    Start,
    /** Game Warm */
    Warm,
    /** Random select */
    Enemy,
    /** Battle loop */
    Player,
    /** Two Remaining */
    Correct,
    /** Winner focus */
    Incorrect,
    /** Game end */
    End
}
public class StateManager : Singleton<StateManager>
{
    private GameState state;
    public GameState State
    {
        get { return state; }
        set { SetState(value); }
    }

    private Notifier notifier;
	public const string ON_STATE_ENTER = "OnStateEnter";
    public const string ON_STATE_EXIT = "OnStateExit";

    void Awake()
    {
        notifier = new Notifier();
        state = GameState.Start;
    }

    private void SetState(GameState newState)
    {
        OnExit(state);
        state = newState;
        OnEnter(state);
    }

    private void OnEnter(GameState s)
    {
		notifier.Notify(ON_STATE_ENTER, s);
    }

	private void OnExit(GameState s)
	{
		notifier.Notify(ON_STATE_EXIT, s);
	}

	void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
    
}
