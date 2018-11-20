using UnityEngine;

public class MoverEscupitajo : MonoBehaviour {

	public GameObject humo;

	private Vector3 irA;
	private float velocidad = 0f;

	void Update () {
		if ( velocidad != 0f ) {
			transform.position += transform.forward * velocidad * Time.deltaTime;

			if ( ( irA - transform.position ).magnitude <= .5f ) {
				Instantiate( humo, irA, humo.transform.rotation );
				Destroy( gameObject );
			}
		}
	}

	public void IrA ( Vector3 pos, float vel ) {
		irA = new Vector3( pos.x, transform.position.y, pos.z );
		velocidad = vel;

		transform.LookAt( irA );
	}
}
