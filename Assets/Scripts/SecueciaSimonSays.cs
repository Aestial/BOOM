using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecueciaSimonSays : MonoBehaviour {

	public enum SimonSays {color1, color2, color3, color4, color5, color6, boton1, boton2, boton3, palanca};
	public static List <SimonSays> secuenciaEnemiga;
	public static List <SimonSays> secuenciaJugador;

	public int noMovimientos;

	private int numRan;
	void Awake()
	{
		secuenciaEnemiga = new List <SimonSays> ();
		CreaSecuenciaEnemiga ();
	}

	void Start()
	{
		Debug.Log ("noMovimeintos" + noMovimientos);
		for (int i = 0; i < noMovimientos; i++) {
			Debug.Log ("contador: " + i + "/" + secuenciaEnemiga[i]);
		}
	}

	void CreaSecuenciaEnemiga()
	{
		for (int i = 0; i < noMovimientos; i++) {
			numRan = Random.Range (0, 10);	//10 es el tamaño de nuestro enum
			LlenaListaEnemiga (numRan);
		}
	}

	void LlenaListaEnemiga(int num)
	{
		if (num == 0) {
			secuenciaEnemiga.Add (SimonSays.color1);
		} else if (num == 1) {
			secuenciaEnemiga.Add (SimonSays.color2);
		} else if (num == 2) {
			secuenciaEnemiga.Add (SimonSays.color3);
		} else if (num == 3) {
			secuenciaEnemiga.Add (SimonSays.color4);
		} else if (num == 4) {
			secuenciaEnemiga.Add (SimonSays.color5);
		} else if (num == 5) {
			secuenciaEnemiga.Add (SimonSays.color6);
		} else if (num == 6) {
			secuenciaEnemiga.Add (SimonSays.boton1);
		} else if (num == 7) {
			secuenciaEnemiga.Add (SimonSays.boton2);
		} else if (num == 8) {
			secuenciaEnemiga.Add (SimonSays.boton3);
		} else if (num == 9) {
			secuenciaEnemiga.Add (SimonSays.palanca);
		}
	}
}
