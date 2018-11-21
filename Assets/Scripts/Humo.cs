using UnityEngine;

public class Humo : MonoBehaviour {

	public float duracion = 5f;
	public float danioPorSegundo = 250f;

	private float seg = 0f;

	private Lexa lexa;

	void Start () {
		lexa = GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent<Lexa>();
	}

	void Update () {
		seg += Time.deltaTime;
		duracion -= Time.deltaTime;

		if ( duracion <= 0f )
			Destroy( gameObject );
	}

	void OnTriggerStay ( Collider other ) {
		Debug.Log( seg );
		if ( other.gameObject.tag == "Player" && seg >= 1f ) {
			Debug.Log( "Daniar" );
			lexa.daniar( danioPorSegundo );
			seg = 0f;
		}
	}
}
