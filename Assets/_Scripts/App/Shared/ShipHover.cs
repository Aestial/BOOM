using UnityEngine;

public class ShipHover : MonoBehaviour 
{
	[Header("Rotation Settings")]
	[SerializeField] private float rotationSpeed = 80.0f;
	[Header("Hover Settings")]
	[SerializeField] private float amplitude = 1.0f;
	[SerializeField] private float refreshRate = 1.0f;
	[SerializeField] private float hoverSpeed = 5.0f;
	[SerializeField] private float noiseIntensity = 0.5f;
	
	private float height, currentTime;
	private Vector3 initialPosition, finalPosition;

	void Start ()
	{
		InvokeRepeating(nameof(GetNewHeight), refreshRate, refreshRate);
	}

	private void GetNewHeight()
	{
		initialPosition = transform.localPosition;
		float offset = noiseIntensity * Random.Range(0.1f, 0.9f);
		height = amplitude * Mathf.Sin(hoverSpeed * Time.time) * offset;
		finalPosition = new Vector3(0, height);
		currentTime = 0.0f;
	}

	void Update () 
	{
		transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, currentTime);
		currentTime += Time.deltaTime;
		transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime);
	}
}
