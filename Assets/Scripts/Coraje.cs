using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( LineRenderer ) )]
[RequireComponent( typeof( Rigidbody ) )]
public class Coraje : MonoBehaviour {

	public float velocidad = 3f;
	public float distancia = 2.5f;
	public float velVuelo = 3f;

	public float distanciaSostener = 1f;
	public float rayoLoco = .25f;

	public float puntosPorDNA = .5f;

	private Transform lexa;
	private Transform centro;
	private Camera camara;

	private Transform objetivo;
	private Transform punto;
	private Recolectable recolectable;

	private LineRenderer lr;
	private Rigidbody rb;

	private enum Estado {
		LEXA, BUSCAR, CENTRO
	};

	private Estado estado;

	private float tiempo = 0;

	private Image progreso;
	private Text porcentaje;

	private float p = 0;

	void Start () {
		progreso = GameObject.Find( "ProgresoCura" ).GetComponent<Image>();
		porcentaje = GameObject.Find( "Porcentaje" ).GetComponent<Text>();

		lexa = GameObject.FindGameObjectsWithTag( "Player" )[0].transform;
		centro = GameObject.FindGameObjectsWithTag( "Centro" )[0].transform;
		camara = GameObject.FindGameObjectsWithTag( "MainCamera" )[0].GetComponent<Camera>();

		lr = GetComponent<LineRenderer>();
		rb = GetComponent<Rigidbody>();

		objetivo = lexa;
		punto = transform.Find( "Punto" );
		estado = Estado.LEXA;
	}

	void ModificarRayo () {
		lr.enabled = true;

		Vector3 punto2 = punto.position + punto.forward * distanciaSostener;

		Vector3 z = punto.position + punto.forward * distanciaSostener * Random.value;
		Vector3 x = punto.right * ( Random.value * 2 - 1 ) * rayoLoco;
		Vector3 y = punto.up * ( Random.value * 2 - 1 ) * rayoLoco;

		Vector3 pMedio = z + x + y;

		lr.SetPosition( 0, punto.position );
		lr.SetPosition( 1, pMedio );
		lr.SetPosition( 2, punto2 );
	}

	void Update () {
		tiempo += Time.deltaTime * velVuelo;
		transform.position = new Vector3( transform.position.x, Mathf.Lerp( .25f, .75f, ( Mathf.Sin( tiempo ) + 1 ) / 2 ), transform.position.z );

		if ( Input.GetMouseButtonDown( 1 ) && estado != Estado.CENTRO ) {
			RaycastHit hit;
			Ray ray = camara.ScreenPointToRay( Input.mousePosition );

			if ( Physics.Raycast( ray, out hit, Mathf.Infinity, LayerMask.GetMask( "Recolectable" ), QueryTriggerInteraction.UseGlobal ) )
				if ( hit.transform.gameObject.tag.CompareTo( "Recolectable" ) == 0 ) {
					objetivo = hit.transform;
					estado = Estado.BUSCAR;
				}
		}

		if ( estado == Estado.CENTRO && ( transform.position - centro.transform.position ).magnitude <= 3f )
			Entregar();
	}
	
	void FixedUpdate () {
		if ( estado == Estado.CENTRO )
			ModificarRayo();
		else {
			lr.enabled = false;
			lr.SetPosition( 0, transform.position );
			lr.SetPosition( 1, transform.position );
			lr.SetPosition( 2, transform.position );
		}


		if ( objetivo != null ) {
			if ( ( transform.position - objetivo.position ).magnitude >= ( estado == Estado.LEXA ? distancia : 0 ) ) {
				transform.LookAt( new Vector3( objetivo.position.x, transform.position.y, objetivo.position.z ) );
				transform.position += transform.forward * velocidad * Time.deltaTime;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			}
		}
	}

	void OnCollisionEnter ( Collision c ) {
		if ( c.gameObject.tag.CompareTo( "Recolectable" ) == 0 && estado == Estado.BUSCAR ) {
			recolectable = c.gameObject.GetComponent<Recolectable>();
			recolectable.Recolectado( punto, punto.position + punto.forward * distanciaSostener );

			objetivo = centro;
			estado = Estado.CENTRO;
		} else if ( c.gameObject.tag.CompareTo( "Centro" ) == 0 && estado == Estado.CENTRO )
			Entregar();
	}

	void Entregar () {
		recolectable.Entregar();
		AumentarProgreso();

		objetivo = lexa;
		estado = Estado.LEXA;
	}

	void AumentarProgreso () {
		p += puntosPorDNA;

		if ( p >= 100f ) {
			Debug.Log( "Ganaste!" );
		}

		progreso.fillAmount = p / 100;
		porcentaje.text = ( ( int ) p ) + "%";
	}
}
