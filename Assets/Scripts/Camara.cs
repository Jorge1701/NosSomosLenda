using UnityEngine;

public class Camara : MonoBehaviour {

	public Transform objetivo;
	public Vector3 distancia;
	
	void LateUpdate () {
		if ( objetivo != null ) {
			transform.position = objetivo.position + distancia;
			transform.LookAt( objetivo.position );
		}
	}
}
