using UnityEngine;

public class PlanetsController : MonoBehaviour 
{
	[SerializeField] private Transform planetContainer = default;
	[SerializeField] private IntVariable planetCount = default;
	[SerializeField] private PlanetsData data = default;
	
	public int startEnergySteps = 2;

	private GameObject m_CurrentPlanet;
	private int m_Count;

	public int count
	{
		get { return m_Count; }
		set 
		{ 
			m_Count = value;
			planetCount.RuntimeValue = value;
		}
	}

	void Start () 
	{
		m_Count = planetCount.RuntimeValue;
		planetContainer.gameObject.SetActive(true);
	}

	public void NewPlanet()
	{
		ViewPlanet(true);
		int length = data.templates.Length;
		int index = Random.Range(0, length);
		m_CurrentPlanet = Instantiate(data.templates[index].prefab, planetContainer);
        Debug.Log("New Planet: " + data.templates[index].name + " Index: " + index);
    }
	
	public void Destroy()
	{
		ViewPlanet(false);
		Destroy(m_CurrentPlanet);
		count++;
	}

	public void Restart()
	{
		count = 0;
	}

	private void ViewPlanet(bool on)
	{
		planetContainer.gameObject.SetActive(true);
	}
}
