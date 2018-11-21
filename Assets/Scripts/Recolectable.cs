using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class Recolectable : MonoBehaviour {

	public float velVuelo = 3.2f;

	private BoxCollider bc;

	private float tiempo = 0;
	private bool recolectado;
	private float duracion = 60;

	void Start () {
		gameObject.tag = "Recolectable";
		gameObject.layer = 10;
		recolectado = false;
		bc = GetComponent<BoxCollider>();
	}

	void Update () {
		if ( recolectado )
			return;

		duracion -= Time.deltaTime;

		if ( duracion <= 0 )
			Destroy( gameObject );

		tiempo += Time.deltaTime * velVuelo;
		transform.position = new Vector3( transform.position.x, Mathf.Lerp( .4f, .8f, ( Mathf.Sin( tiempo ) + 1 ) / 2 ), transform.position.z );
	}

	public void Recolectado ( Transform padre, Vector3 distancia ) {
		transform.parent = padre;
		transform.position = distancia;
		bc.enabled = false;
		recolectado = true;
	}

	public void Entregar () {
		// TODO: Entregar
		Destroy( gameObject );
	}
}
