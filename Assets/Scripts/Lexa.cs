using UnityEngine;

public class Lexa : MonoBehaviour {

	public float velocidad;

	void FixedUpdate () {
		Vector3 irA = Vector3.zero;

		if ( Input.GetKey( "w" ) )
			irA += new Vector3( 0, 0, 1 );

		if ( Input.GetKey( "s" ) )
			irA += new Vector3( 0, 0, -1 );

		if ( Input.GetKey( "a" ) )
			irA += new Vector3( -1, 0, 0 );

		if ( Input.GetKey( "d" ) )
			irA += new Vector3( 1, 0, 0 );

		transform.position += irA.normalized * velocidad;
		transform.LookAt( transform.position + irA );
	}
}
