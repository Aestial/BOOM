using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaSecuencias : MonoBehaviour {

	private int contador;

	void OnEnable()
	{
		ControlSecuenciaJugador.Verificador += ComparaSecuenciasEnemigoJugador;
	}

	void OnDisable()
	{
		ControlSecuenciaJugador.Verificador -= ComparaSecuenciasEnemigoJugador;
	}

	void ComparaSecuenciasEnemigoJugador()
	{
		if (SecueciaSimonSays.secuenciaJugador [contador] == SecueciaSimonSays.secuenciaEnemiga [contador]) {
			Debug.Log ("Correcto");
			contador++;
		} else {
			//Al pisar una boton mal se marcara como incirrecto, se limpiara la lista y se creara una nueva secuencia
			Debug.Log ("Incorrecto");
			contador = 0;
		}
	}
}
