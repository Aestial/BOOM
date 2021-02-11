using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonAction
{
	Show,
	Push,
}

public class ShipActorController : MonoBehaviour 
{
	[SerializeField] private Color color = default;
	[SerializeField] private float emissionIntensity = default;
	[SerializeField] private AudioClip showSoundFX = default;
	[SerializeField] private AudioClip pushSoundFX = default;

	private Material material;

	public int id;
	public new bool enabled;

	public delegate void ClickAction(string name);
    public event ClickAction OnClicked;

	void Start () 
	{
		material = GetComponent<MeshRenderer>().material;
		Set();
		Illuminate(false);
	}
	void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			Debug.Log("Clicked on the UI");
		}
		else
		{
			if (enabled)
			{
				Push(true);
				Illuminate(true);
			}
		}
  	}
	
	void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			Debug.Log("Clicked on the UI");
		}
		else
		{
			if (enabled)
			{		
				Push(false);
				Illuminate(false);
				PlaySound(ButtonAction.Push, 1.18f);
				PlaySound(ButtonAction.Show, 0.83f);
				// Execute suscripted events:
				OnClicked?.Invoke(this.gameObject.name);
			}
        }
	}

	public void Show(bool on)
	{
		Illuminate(on);
		if (on)
			PlaySound(ButtonAction.Show, 0.9f);
	}

	public void Illuminate(bool on)
	{
		if (on) 
			material.EnableKeyword("_EMISSION");
		else
			material.DisableKeyword("_EMISSION");
	}

	private void Set()
	{
		material.color = this.color;
		material.SetColor("_EmissionColor", this.color * this.emissionIntensity);
	}

	private void Push(bool on)
	{
		if (on)
			transform.localPosition += new Vector3(0.0f, 0.15f);
		else
			transform.localPosition -= new Vector3(0.0f, 0.15f);
	}

    private void PlaySound(ButtonAction action, float volume)
	{
		AudioClip clip;
		switch(action)
		{
			case ButtonAction.Show:
			clip = this.showSoundFX;
			break;
			case ButtonAction.Push:
			clip = this.pushSoundFX;
			break;
			default:
			clip = this.showSoundFX;
			break;
		}
		AudioManager.Instance.PlayOneShoot(clip, volume);
	}

}
