using UnityEngine;

public class Humo : MonoBehaviour {

	public float duracion = 5f;
	public float danioPorSegundo = 10f;

	private Lexa lexa;

	void Start () {
		lexa = GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent<Lexa>();
	}

	void Update () {
		duracion -= Time.deltaTime;

		if ( duracion <= 0 )
			Destroy( gameObject );
	}

	void OnTriggerStay ( Collider other ) {
		if ( other.gameObject.tag == "Player" )
			lexa.daniar( danioPorSegundo * Time.deltaTime );
	}
}
