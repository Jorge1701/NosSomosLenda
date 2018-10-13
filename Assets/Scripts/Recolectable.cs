using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class Recolectable : MonoBehaviour {

	private BoxCollider bc;

	void Start () {
		gameObject.tag = "Recolectable";
		bc = GetComponent<BoxCollider>();
	}

	public void Recolectado ( Transform padre, Vector3 distancia ) {
		transform.parent = padre;
		transform.position = distancia;
		bc.enabled = false;
	}

	public void Entregar () {
		// TODO: Entregar
		Destroy( gameObject );
	}
}
