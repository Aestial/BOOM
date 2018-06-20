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

	// Use this for initialization
	void Start () 
	{
		this.renderer = this.GetComponent<MeshRenderer>();
		this.renderer.enabled = false;
		this.StartAnimation();
	}

	// void Update () 
	// {
	// 	if (Input.GetKeyDown(KeyCode.Space))
	// 	{
	// 		this.Shoot();
	// 	}
	// }

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
		this.time = 0.0f;
		this.renderer.enabled = true;

		Vector3 startPosition = this.startPoint.position;
		Vector3 endPosition = this.endPoint.position;

		Vector3 startScale = this.startPoint.localScale;
		Vector3 endScale = this.endPoint.localScale;

		float trailStartScale = this.startPoint.localScale.x;
		float trailEndScale = this.endPoint.localScale.x;
			
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
		Debug.Log("Bullet Animation End");
		this.renderer.enabled = false;
		if (this.AnimationEnd != null)
			this.AnimationEnd();
	}
}
