using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	public BarraVida barraVida;
	public float vida;
	public float vidaMax;
	// Use this for initialization

	void Start () {

		barraVida = GameObject.FindGameObjectsWithTag( "vidaBase" )[0].GetComponent<BarraVida>();
		barraVida.Max( vida );
		barraVida.Vida( vida );	
	}



	public void daniar( float danio ) {
		if ( vida <= 0f )
			return;

		if ( danio == -1f ) {
			if ( vida / vidaMax <= .50f )
				vida = 0;
			else
				vida = 20f;
		} else
			vida -= danio;

		barraVida.Vida( vida );

	}
}
