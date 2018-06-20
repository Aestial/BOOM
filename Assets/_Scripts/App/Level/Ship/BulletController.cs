using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
	[SerializeField] private Transform startPoint;
	[SerializeField] private Transform endPoint;
	[SerializeField] private float animationTime;
	[SerializeField] private float animationInterval;
	[SerializeField] private TrailRenderer trail;

	public delegate void AnimationEndAction();
	public event AnimationEndAction AnimationEnd;

	private float time;
	private new MeshRenderer renderer;

	void Start () 
	{
		this.renderer = this.GetComponent<MeshRenderer>();
		this.renderer.enabled = false;
		this.trail.enabled = false;
	}

	public void Shoot()
	{
		this.StartAnimation();
	}

	private void StartAnimation()
	{
		StartCoroutine(this.AnimationCoroutine());
	}

	private IEnumerator AnimationCoroutine()
	{
		Vector3 startPosition = this.startPoint.position;
		Vector3 endPosition = this.endPoint.position;

		Vector3 startScale = this.startPoint.localScale;
		Vector3 endScale = this.endPoint.localScale;

		float trailStartScale = this.startPoint.localScale.x;
		float trailEndScale = this.endPoint.localScale.x;
		
		this.time = 0.0f;
		this.renderer.enabled = true;
		this.trail.enabled = true;

		yield return null;
			
		while (true)
		{
			yield return new WaitForSeconds(this.animationInterval);
			
			this.time += this.animationInterval;
			
			this.transform.position = Vector3.Lerp(startPosition, endPosition, this.time);
			this.transform.localScale = Vector3.Lerp(startScale, endScale, this.time);
			this.trail.widthMultiplier = Mathf.Lerp(trailStartScale, trailEndScale, this.time);

			if (Mathf.Approximately(Vector3.Distance(this.transform.position, endPosition), 0.0f))
			{
				this.AnimationEndCallback();
				break;
			}
		}
	}

	private void AnimationEndCallback()
	{
		this.renderer.enabled = false;
		this.trail.enabled = false;

		this.transform.position = this.startPoint.position;
		this.transform.localScale = this.startPoint.localScale;

		if (this.AnimationEnd != null)
			this.AnimationEnd();
	}
}
