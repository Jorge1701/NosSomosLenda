using UnityEngine;

[RequireComponent( typeof( LineRenderer ) )]
public class Lexa : MonoBehaviour {

	public GameObject choqueBala;

	public float vidaMax = 250;
	private float vida;

	public float danio = 45;

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
	private Transform direccion;
	private LineRenderer lr;

	private Transform disparo;

	private Animator anim;

	void Start () {
		vida = vidaMax;

		camara = GameObject.FindGameObjectsWithTag( "MainCamera" )[0].GetComponent<Camera>();
		lr = GetComponent<LineRenderer>();
		disparo = transform.Find( "Disparo" );

		if ( GameObject.Find( "Direccion" ) == null )
			Debug.LogWarning( "Cambiar camara por el prefab 'Camara Principal'" );
		else
			direccion = GameObject.Find( "Direccion" ).transform;

		anim = transform.Find( "Modelo" ).GetComponent<Animator>();
	}

	void Update () {
		if ( vida <= 0 )
			return;

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
					Vector3 puntoFinal = disparo.position + disparo.forward * ( disparoDist + Random.value * 1.5f );
					puntoFinal.y = disparo.position.y;

					RaycastHit hit2;
					Ray ray2 = new Ray( disparo.position, disparo.forward );

					if ( Physics.Raycast( ray2, out hit2, disparoDist + 1.5f ) ) {
						if ( hit2.transform.gameObject.tag.CompareTo( "Zombie" ) == 0 ) {
							ZombieController z = hit2.transform.GetComponent<ZombieController>();
							z.daniar( danio );
						}

						GameObject bala = Instantiate( choqueBala, hit2.point, choqueBala.transform.rotation ) as GameObject;
						bala.transform.LookAt( transform.position );

						puntoFinal = new Vector3( hit2.point.x, disparo.position.y, hit2.point.z );
					}

					lr.enabled = true;
					lr.SetPosition( 0, disparo.position );
					lr.SetPosition( 1, puntoFinal );
				}
			}
		}
	}

	void FixedUpdate () {
		if ( vida <= 0 )
			return;

		Vector3 irA = Vector3.zero;

		if ( Input.GetKey( "w" ) )
			irA += direccion.forward;

		if ( Input.GetKey( "s" ) )
			irA -= direccion.forward;

		if ( Input.GetKey( "a" ) )
			irA -= direccion.right;

		if ( Input.GetKey( "d" ) )
			irA += direccion.right;

		float vel = velocidad;

		anim.SetBool( "Caminar", irA.magnitude > 0 );

		if ( Input.GetKey( "left shift" ) && irA.magnitude > 0 ) {
			vel *= multiVel;
			anim.SetBool( "Correr", true );
		} else
			anim.SetBool( "Correr", false );

		transform.position += irA.normalized * vel * Time.deltaTime;

		if ( puedeGirar <= 0 )
			transform.LookAt( transform.position + irA );
	}

	public void daniar( float danio ) {
		if ( vida <= 0 )
			return;

		if ( danio == -1 )
			vida = 5;
		else
			vida -= danio;

		if ( vida <= 0 )
			Destroy( gameObject, 3f );
	}
}