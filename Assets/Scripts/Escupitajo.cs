using UnityEngine;

public class Escupitajo : MonoBehaviour {

	public GameObject escupir;
	public float velocidad = 3f;

	private Transform jugador;

	void Start () {
		jugador = GameObject.FindGameObjectsWithTag( "Player" )[0].transform;
	}
	
	public void Escupir () {
		if ( escupir )
			Instantiate( escupir, transform.position, transform.rotation ).GetComponent<MoverEscupitajo>().IrA( jugador.transform.position, velocidad );
	}
}
