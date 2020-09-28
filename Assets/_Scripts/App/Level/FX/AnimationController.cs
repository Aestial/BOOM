using UnityEngine;

public class AnimationController : MonoBehaviour 
{
	[SerializeField] private string animationName = default;
	private Animator animator;

	public delegate void AnimationEventAction(string name);
	public event AnimationEventAction OnAnimationEvent;

	public delegate void AnimationEndAction();
	public event AnimationEndAction OnAnimationEnd;

	void Start ()
	{
		animator = GetComponent<Animator>();
	}

	public void Play()
	{
		animator.Play(animationName, -1, 0f);
	}

	public void Stop()
	{
		animator.StopPlayback();
	}

	public void AnimationEndReached()
	{
        OnAnimationEnd?.Invoke();
    }

	public void AnimationEvent(string name)
	{
        OnAnimationEvent?.Invoke(name);
    }
}
