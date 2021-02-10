using UnityEngine;

public class AnimationController : MonoBehaviour 
{
	[SerializeField] private string animationName = default;
	private Animator animator;

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
}
