using UnityEngine;

public class PlanetsController : MonoBehaviour 
{
	[SerializeField] private Transform container = default;
	[SerializeField] private IntVariableCallback planetCount = default;
	[SerializeField] private TemplateCollection templateCollection = default;
	
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
		container.gameObject.SetActive(true);
	}

	public void NewPlanet()
	{
		ViewPlanet(true);
		int length = templateCollection.templates.Length;
		int index = Random.Range(0, length);
		var prefab = templateCollection.templates[index].prefab;
		m_CurrentPlanet = Instantiate(prefab, container);
        Debug.Log("New Planet: " + templateCollection.templates[index].name + " Index: " + index);
    }

	private void DestroyPlanet()
    {
		ViewPlanet(false);
		Destroy(m_CurrentPlanet);
	}
	
	public void Destroy()
	{
		count++;
		DestroyPlanet();
	}

	public void Restart()
	{
		count = 0;
		DestroyPlanet();
	}

	private void ViewPlanet(bool on)
	{
		container.gameObject.SetActive(true);
	}
}
