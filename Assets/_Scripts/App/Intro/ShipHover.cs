using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHover : MonoBehaviour 
{
	[Header("Rotation Settings")]
	[SerializeField] private float rotationSpeed;
	[Header("Hover Settings")]
	[SerializeField] private float amplitude;
	[SerializeField] private float refreshRate;
	[SerializeField] private float hoverSpeed;
	[SerializeField] private float noiseIntensity;
	
	private float height, currentTime;
	private Vector3 initialPosition, finalPosition;

	void Start ()
	{
		InvokeRepeating("GetNewHeight", this.refreshRate, this.refreshRate);
	}

	private void GetNewHeight()
	{
		this.initialPosition = this.transform.localPosition;
		float offset = noiseIntensity * Random.Range(0.1f, 0.9f);
		this.height = this.amplitude * Mathf.Sin(this.hoverSpeed * Time.time) * offset;
		this.finalPosition = new Vector3(0, this.height);
		this.currentTime = 0.0f;
	}

	void Update () 
	{
		this.transform.localPosition = Vector3.Lerp(this.initialPosition, this.finalPosition, this.currentTime);
		this.currentTime += Time.deltaTime;
		this.transform.eulerAngles += new Vector3(0, this.rotationSpeed * Time.deltaTime);
	}
}
