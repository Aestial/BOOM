using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSecuenciaJugador : MonoBehaviour {
	/*El Scrip nos ayuda a crear la lista del Jugador, la cual se comparara con la del enemigo y así poder subir o bajar la barra de vida*/

	public delegate void EventosVariosControl ();
	public static event EventosVariosControl Verificador;

	//Palanca
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

	void Awake()
	{
		SecueciaSimonSays.secuenciaJugador = new List<SecueciaSimonSays.SimonSays> ();
	}

	public void BotonC1()
	{
		casilla1.GetComponent<Image> ().color = new Color (1f, 0.84f, 0f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.color1);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonC2()
	{
		casilla2.GetComponent<Image> ().color = new Color (0.27f, 0.87f, 0.2f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.color2);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonC3()
	{
		casilla3.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.color3);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonC4()
	{
		casilla4.GetComponent<Image> ().color = new Color (1f, 0.43f, 0f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.color4);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonC5()
	{
		casilla5.GetComponent<Image> ().color = new Color (0f, 0.33f, 1f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.color5);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonC6()
	{
		casilla6.GetComponent<Image> ().color = new Color (1f, 0f, 0.6f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.color6);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonA()
	{
		botonA.GetComponent<Image> ().color = new Color (0f, 0.75f, 1f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.boton1);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonB()
	{
		botonB.GetComponent<Image> ().color = new Color (1f, 0.43f, 0f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.boton2);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonC()
	{
		botonC.GetComponent<Image> ().color = new Color (0f, 0.6f, 0.09f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.boton3);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	public void BotonPalanca()
	{
		palanca.GetComponent<Image> ().color = new Color (0.68f, 0.68f, 0.68f, 1f);
		SecueciaSimonSays.secuenciaJugador.Add (SecueciaSimonSays.SimonSays.palanca);
		StartCoroutine (RegresaColor ());
		if (Verificador != null)
			Verificador ();
	}

	IEnumerator RegresaColor()
	{
		yield return new WaitForSeconds (0.15f);
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
	}

}
