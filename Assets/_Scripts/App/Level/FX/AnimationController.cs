using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour 
{
	[SerializeField] private string animationName;
	private Animator animator;

	public delegate void AnimationEventAction(string name);
	public event AnimationEventAction OnAnimationEvent;

	public delegate void AnimationEndAction();
	public event AnimationEndAction OnAnimationEnd;

	void Start ()
	{
		this.animator = this.GetComponent<Animator>();
	}

	public void Play()
	{
		this.animator.Play(this.animationName, -1, 0f);
	}

	public void Stop()
	{
		this.animator.StopPlayback();
	}

	public void AnimationEndReached()
	{
		if (this.OnAnimationEnd != null)
		{
			this.OnAnimationEnd();
		}
	}

	public void AnimationEvent(string name)
	{
		if (this.OnAnimationEvent != null)
		{
			this.OnAnimationEvent(name);
		}
	}
}
