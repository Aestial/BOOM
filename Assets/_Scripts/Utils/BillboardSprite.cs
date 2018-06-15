using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BillboardSprite : MonoBehaviour 
{
	[SerializeField] private bool useMainCamera = true;
	[SerializeField] private bool isCameraMoving = false;
	[SerializeField] private new Camera camera;
	private Camera mCamera;

	void Start () 
	{
		// Assign correct camera
		if (useMainCamera)
			this.mCamera = Camera.main;
		else 
			this.mCamera = this.camera;
		// Billboard
		this.transform.LookAt(this.mCamera.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Moving camera (billboard effect)
		if (this.isCameraMoving)
		{
			this.transform.LookAt(this.mCamera.transform.position);
		}
	}
}
