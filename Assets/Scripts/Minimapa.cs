using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimapa : MonoBehaviour {

    public Transform jugador;
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 nuevaPosicion = jugador.position;
        nuevaPosicion.y = transform.position.y;
        transform.position = nuevaPosicion;
	}
}
