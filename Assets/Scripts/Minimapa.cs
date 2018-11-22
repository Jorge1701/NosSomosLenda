using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimapa : MonoBehaviour {

    private Transform jugador;

    void Start () {
    	jugador = GameObject.FindGameObjectsWithTag( "Player" )[0].transform;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 nuevaPosicion = jugador.position;
        nuevaPosicion.y = transform.position.y;
        transform.position = nuevaPosicion;
	}
}
