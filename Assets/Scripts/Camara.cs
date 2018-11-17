using UnityEngine;

public class Camara : MonoBehaviour {

	private Transform objetivo;
	public Vector3 distancia;

	private Transform direccion;

	void Start () {
		direccion = GameObject.Find( "Direccion" ).transform;
		objetivo = GameObject.Find( "Lexa" ).transform;
	}
	
	void LateUpdate () {
		if ( objetivo != null ) {
			transform.position = objetivo.position + distancia;
			transform.LookAt( objetivo.position );
		}

		direccion.LookAt( transform.position + new Vector3( transform.forward.x, 0, transform.forward.z ) );
	}
}
