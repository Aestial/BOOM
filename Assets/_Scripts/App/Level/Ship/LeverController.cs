using UnityEngine;

public class LeverController : MonoBehaviour 
{

	[SerializeField] private float maxRotation;
	[SerializeField] private AudioClip pullSoundFX;
	[SerializeField] private AudioClip releaseSoundFX;
	[SerializeField] private GameObject helpCanvas;

	public new bool enabled
	{
		get { return m_Enabled; }
		set 
		{
			helpCanvas.SetActive(value);
			m_Enabled = value;
		}
	}
	private bool m_Enabled;

	public delegate void ClickAction();
	public event ClickAction OnClicked;

	// Use this for initialization
	void Start () 
	{
		this.Set();
	}

	void OnMouseDown()
	{
		this.Pull(true);
		this.PlaySound(this.pullSoundFX);
		
		// Execute suscripted events:
		if(this.OnClicked != null && this.enabled)
		{
			this.enabled = false;
			this.OnClicked();
		}
	}
	
	void OnMouseUp()
	{
		this.Pull(false);
		this.PlaySound(this.releaseSoundFX);
	}

	private void Set()
	{
		this.enabled = false;
		this.transform.parent.localEulerAngles = Vector3.zero;
	}

	private void Pull(bool on)
	{
		if (on)
			this.transform.parent.localEulerAngles += new Vector3(this.maxRotation, 0);
		else
			this.transform.parent.localEulerAngles -= new Vector3(this.maxRotation, 0);
	}
	
	private void PlaySound (AudioClip clip) 
	{
		AudioManager.Instance.PlayOneShoot(clip);
	}
}
