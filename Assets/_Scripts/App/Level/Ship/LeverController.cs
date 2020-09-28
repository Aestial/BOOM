using UnityEngine;

public class LeverController : MonoBehaviour 
{

	[SerializeField] private float maxRotation = 30.0f;
	[SerializeField] private AudioClip pullSoundFX = default;
	[SerializeField] private AudioClip releaseSoundFX = default;
	[SerializeField] private GameObject helpCanvas = default;

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
		Set();
	}

	void OnMouseDown()
    {
        Pull(true);
        PlaySound(pullSoundFX);

        // Execute suscripted events:
        if (OnClicked == null || !enabled)
        {
            return;
        }
        enabled = false;
        OnClicked();
    }

    void OnMouseUp()
	{
		Pull(false);
        PlaySound(releaseSoundFX);
	}

	private void Set()
	{
		enabled = false;
		transform.parent.localEulerAngles = Vector3.zero;
	}

	private void Pull(bool on)
	{
		if (on)
			transform.parent.localEulerAngles += new Vector3(this.maxRotation, 0);
		else
			transform.parent.localEulerAngles -= new Vector3(this.maxRotation, 0);
	}
	
	private void PlaySound (AudioClip clip) 
	{
		AudioManager.Instance.PlayOneShoot(clip);
	}
}
