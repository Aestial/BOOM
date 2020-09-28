using UnityEngine;

[ExecuteInEditMode]
public class BillboardSprite : MonoBehaviour 
{
	[SerializeField] private bool useMainCamera = true;
	[SerializeField] private bool isCameraMoving = false;
	[SerializeField] private new Camera camera = default;
	private Camera mCamera;

	void Start () 
	{
		// Assign correct camera
		if (useMainCamera)
			mCamera = Camera.main;
		else 
			mCamera = camera;
		// Billboard
		transform.LookAt(mCamera.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Moving camera (billboard effect)
		if (isCameraMoving)
		{
			transform.LookAt(mCamera.transform.position);
		}
	}
}
