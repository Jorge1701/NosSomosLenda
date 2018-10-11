using UnityEngine;

public class Camara : MonoBehaviour {

	public Transform objetivo;
	[Range( 0f, 1f )]
	public float avance;
	public Vector3 distancia;
	
	void LateUpdate () {
		if ( objetivo != null ) {
			transform.position = Vector3.Lerp( transform.position, objetivo.position, avance ) + distancia;
			transform.LookAt( objetivo.position );
		}
	}
}
