using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayAnimationInController : MonoBehaviour 
{
	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator>();
	}

	public void Play(string name)
	{
		animator.Play(name, -1, 0f);
	}

	public void Stop()
	{
		animator.StopPlayback();
	}
}
