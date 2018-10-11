using UnityEngine;

public class Coraje : MonoBehaviour {

	public Transform lexa;
	public Transform centro;
	public float velocidad;
	public float distancia;

	public Camera camara;

	private Transform objetivo;
	private Transform boca;
	private Recolectable recolectable;

	private enum Estado {
		LEXA, BUSCAR, CENTRO
	};

	private Estado estado;

	void Start () {
		objetivo = lexa;
		boca = transform.Find( "Boca" );
		estado = Estado.LEXA;
	}

	void Update () {
		if ( Input.GetMouseButtonDown( 1 ) && estado != Estado.CENTRO ) {
			RaycastHit hit;
			Ray ray = camara.ScreenPointToRay( Input.mousePosition );

			if ( Physics.Raycast( ray, out hit ) )
				if ( hit.transform.gameObject.tag.CompareTo( "Recolectable" ) == 0 ) {
					objetivo = hit.transform;
					estado = Estado.BUSCAR;
				}
		}
	}
	
	void FixedUpdate () {
		if ( objetivo != null ) {

			if ( ( transform.position - objetivo.position ).magnitude >= ( estado == Estado.LEXA ? distancia : 0 ) ) {
				transform.LookAt( new Vector3( objetivo.position.x, transform.position.y, objetivo.position.z ) );
				transform.position += transform.forward * velocidad;
			}
		}
	}

	void OnCollisionEnter ( Collision c ) {
		if ( c.gameObject.tag.CompareTo( "Recolectable" ) == 0 && estado == Estado.BUSCAR ) {
			recolectable = c.gameObject.GetComponent<Recolectable>();
			recolectable.Recolectado( boca );

			objetivo = centro;
			estado = Estado.CENTRO;
		} else if ( c.gameObject.tag.CompareTo( "Centro" ) == 0 && estado == Estado.CENTRO ) {
			recolectable.Entregar();

			objetivo = lexa;
			estado = Estado.LEXA;
		}
	}
}
