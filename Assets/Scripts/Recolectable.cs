using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
[RequireComponent( typeof( BoxCollider ) )]
public class Recolectable : MonoBehaviour {

	private Rigidbody rb;
	private BoxCollider bc;

	void Start () {
		gameObject.tag = "Recolectable";
		rb = GetComponent<Rigidbody>();
		bc = GetComponent<BoxCollider>();
	}

	public void Recolectado ( Transform padre ) {
		transform.parent = padre;
		transform.localPosition = Vector3.zero;
		rb.useGravity = false;
		bc.enabled = false;
	}

	public void Entregar () {
		// TODO: Entregar
		Destroy( gameObject );
	}
}
