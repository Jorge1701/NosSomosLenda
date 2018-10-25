using UnityEngine;

[RequireComponent( typeof( LineRenderer ) )]
public class Lexa : MonoBehaviour {

	public GameObject choqueBala;

	public float danio = 20;

	public float disparoDist = 15f;
	public float velocidad = .1f;
	public float multiVel = 1.5f;

	public float velDisparo = .25f;
	private float sigDisparo = 0;

	private float mostrarDisparo = .01f;
	private float ocultarDisparo = 0f;

	private float tiempoGiro = .5f;
	private float puedeGirar = 0f;

	private Camera camara;
	private LineRenderer lr;

	private Transform disparo;

	private Animator anim;

	void Start () {
		camara = GameObject.FindGameObjectsWithTag( "MainCamera" )[0].GetComponent<Camera>();
		lr = GetComponent<LineRenderer>();
		disparo = transform.Find( "Disparo" );

		anim = transform.Find( "Modelo" ).GetComponent<Animator>();
	}

	void Update () {
		if ( ocultarDisparo <= 0 ) {
			lr.enabled = false;
			lr.SetPosition( 0, transform.position );
			lr.SetPosition( 1, transform.position );
		}

		sigDisparo += Time.deltaTime;
		ocultarDisparo -= Time.deltaTime;
		puedeGirar -= Time.deltaTime;

		bool disparando = Input.GetMouseButton( 0 );

		anim.SetBool( "Disparar", disparando );

		if ( sigDisparo >= velDisparo ) {
			sigDisparo = 0;

			if ( disparando ) {
				RaycastHit hit;
				Ray ray = camara.ScreenPointToRay( Input.mousePosition );

				if ( Physics.Raycast( ray, out hit, Mathf.Infinity, LayerMask.GetMask( "Balas" ) ) ) {
					transform.LookAt( new Vector3( hit.point.x, transform.position.y, hit.point.z ) );

					puedeGirar = tiempoGiro;
					ocultarDisparo = mostrarDisparo;
					lr.enabled = true;
					lr.SetPosition( 0, disparo.position );
					Vector3 puntoFinal = disparo.position + disparo.forward * ( disparoDist + Random.value * 1.5f );
					puntoFinal.y = disparo.position.y;
					lr.SetPosition( 1, puntoFinal );

					RaycastHit hit2;
					Ray ray2 = new Ray( disparo.position, disparo.forward );

					if ( Physics.Raycast( ray2, out hit2, disparoDist + 1.5f ) ) {
						if ( hit2.transform.gameObject.tag.CompareTo( "Zombie" ) == 0 ) {
							ZombieController z = hit2.transform.GetComponent<ZombieController>();
							z.daniar( danio );
						}
						GameObject bala = Instantiate( choqueBala, hit2.point, choqueBala.transform.rotation ) as GameObject;
						bala.transform.LookAt( transform.position );
					}
				}
			}
		}
	}

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

		float vel = velocidad;

		anim.SetBool( "Caminar", irA.magnitude > 0 );

		if ( Input.GetKey( "left shift" ) ) {
			vel *= multiVel;
			anim.SetBool( "Correr", true );
		} else
			anim.SetBool( "Correr", false );

		transform.position += irA.normalized * vel * Time.deltaTime;

		if ( puedeGirar <= 0 )
			transform.LookAt( transform.position + irA );
	}
}
