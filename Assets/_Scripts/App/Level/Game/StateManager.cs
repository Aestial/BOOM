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
    /** Wait for shoot and destroy */
    Shoot,
    /** Winner, planet destroyed */
    Winner,
    /** Loser, ship destroyed */
    Loser,
    /** Game end */
    End
}

public class StateManager : Singleton<StateManager>
{
    private GameState state;
    public GameState State
    {
        get { return this.state; }
        set { this.SetState(value); }
    }

    private Notifier notifier;
	public const string ON_STATE_ENTER = "OnStateEnter";
    public const string ON_STATE_EXIT = "OnStateExit";

    void Awake()
    {
        this.notifier = new Notifier();
    }

    private void SetState(GameState newState)
    {
        OnExit(this.state);
        this.state = newState;
        OnEnter(this.state);
    }

    private void OnEnter(GameState s)
    {
		this.notifier.Notify(ON_STATE_ENTER, s);
    }

	private void OnExit(GameState s)
	{
		this.notifier.Notify(ON_STATE_EXIT, s);
	}

	void OnDestroy()
    {
        this.notifier.UnsubcribeAll();
    }
    
}
