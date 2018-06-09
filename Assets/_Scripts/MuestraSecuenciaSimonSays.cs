using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuestraSecuenciaSimonSays : MonoBehaviour 
{
	[System.Serializable]
	public struct ShipButton 
	{
		public string name;
		public GameObject go;
		public Color normalColor;
		public Color activeColor;
		public Texture2D texture;
	}

	[SerializeField] private ShipButton[] buttons;

	//Palnaca
	public Button palanca;
	//Casillas
	public Button casilla1;
	public Button casilla2;
	public Button casilla3;
	public Button casilla4;
	public Button casilla5;
	public Button casilla6;
	//Botones
	public Button botonA;
	public Button botonB;
	public Button botonC;

	public bool bandera;
	private int cont;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (bandera) {
			if (cont < SecueciaSimonSays.secuenciaEnemiga.Count) {
				StartCoroutine (VisualizaSecuencia ());
			}
		}
	}

	IEnumerator VisualizaSecuencia()
	{
		bandera = false;
		if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.color1) {
			casilla1.GetComponent<Image> ().color = new Color (1f, 0.84f, 0f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.color2) {
			casilla2.GetComponent<Image> ().color = new Color (0.27f, 0.87f, 0.2f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.color3) {
			casilla3.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.color4) {
			casilla4.GetComponent<Image> ().color = new Color (1f, 0.43f, 0f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.color5) {
			casilla5.GetComponent<Image> ().color = new Color (0f, 0.33f, 1f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.color6) {
			casilla6.GetComponent<Image> ().color = new Color (1f, 0f, 0.6f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.boton1) {
			botonA.GetComponent<Image> ().color = new Color (0f, 0.75f, 1f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.boton2) {
			botonB.GetComponent<Image> ().color = new Color (1f, 0.43f, 0f, 1f);
		}else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.boton3) {
			botonC.GetComponent<Image> ().color = new Color (0f, 0.6f, 0.09f, 1f);
		} else if (SecueciaSimonSays.secuenciaEnemiga [cont] == SecueciaSimonSays.SimonSays.palanca) {
			palanca.GetComponent<Image> ().color = new Color (0.68f, 0.68f, 0.68f, 1f);
		}
		yield return new WaitForSeconds (1.5f);
		yield return StartCoroutine (RegresaColor ());
		cont++;
		bandera = true;
	}

	IEnumerator RegresaColor()
	{
		casilla1.GetComponent<Image> ().color = new Color (1f, 0.84f, 0f, 0.5f);
		casilla2.GetComponent<Image> ().color = new Color (0.27f, 0.87f, 0.2f, 0.5f);
		casilla3.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 0.5f);
		casilla4.GetComponent<Image> ().color = new Color (1f, 0.43f, 0f, 0.5f);
		casilla5.GetComponent<Image> ().color = new Color (0f, 0.33f, 1f, 0.5f);
		casilla6.GetComponent<Image> ().color = new Color (1f, 0f, 0.6f, 0.5f);
		botonA.GetComponent<Image> ().color = new Color (0f, 0.75f, 1f, 0.5f);
		botonB.GetComponent<Image> ().color = new Color (1f, 0.43f, 0f, 0.5f);
		botonC.GetComponent<Image> ().color = new Color (0f, 0.6f, 0.09f, 0.5f);
		palanca.GetComponent<Image> ().color = new Color (0.68f, 0.68f, 0.68f, 0.5f);
		yield return new WaitForSeconds (1f);
	}

}
