using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour 
{
	[SerializeField] private Transform startPoint = default;
	[SerializeField] private Transform endPoint = default;
	[SerializeField] private float animationInterval = 0.01f;
	[SerializeField] private TrailRenderer trail = default;

	[SerializeField]
	UnityEvent OnAnimationEnd;

	private float time;
	private new MeshRenderer renderer;

	void Start () 
	{
		renderer = GetComponent<MeshRenderer>();
		renderer.enabled = false;
		trail.enabled = false;
	}

	public void Shoot()
	{
		StartCoroutine(AnimationCoroutine());
	}

	private IEnumerator AnimationCoroutine()
	{
		Vector3 startPosition = startPoint.position;
		Vector3 endPosition = endPoint.position;

		Vector3 startScale = startPoint.localScale;
		Vector3 endScale = endPoint.localScale;

		float trailStartScale = startPoint.localScale.x;
		float trailEndScale = endPoint.localScale.x;
		
		time = 0.0f;
		renderer.enabled = true;
		trail.enabled = true;

		yield return null;
			
		while (true)
		{
			yield return new WaitForSeconds(animationInterval);
			
			time += animationInterval;
			
			transform.position = Vector3.Lerp(startPosition, endPosition, time);
			transform.localScale = Vector3.Lerp(startScale, endScale, time);
			trail.widthMultiplier = Mathf.Lerp(trailStartScale, trailEndScale, time);

			if (Mathf.Approximately(Vector3.Distance(transform.position, endPosition), 0.0f))
			{
				AnimationEndCallback();
				break;
			}
		}
	}

	private void AnimationEndCallback()
	{
		renderer.enabled = false;
		trail.enabled = false;

		transform.position = startPoint.position;
		transform.localScale = startPoint.localScale;

		OnAnimationEnd?.Invoke();
    }
}
