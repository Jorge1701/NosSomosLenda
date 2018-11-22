using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Spawn : MonoBehaviour {

	public int zombiesEnOleada = 20;
	public int zombiesEnOleadaInc = 10;
	public int zombiesALaVez = 4;
	public int zombiesALaVezInc = 2;

	public float zombieNormal = .65f;
	public float zombieEscupidor = .9f;

	public float tiempoEntreDias = 5f;

	public Gradient luz;

	private Light sol;
	private float tiempo = 0f;
	private bool esDia = false;

	private GameObject[] zombies;
	private Transform[] spawns;
	private int zombiesOleadaMax = 0;
	private int zombiesVivos = 0;
	private int zombiesMatados = 0;

	private int oleada = 1;
	private float tiempoSigDia = 5f;

	private bool dia = false;

	private MarcadorZombies marcador = null;

	private Text txtDia;
	private Text sigDia;

	private Lexa lexa;

	void Start () {
		lexa = GameObject.FindGameObjectsWithTag( "Player" )[0].GetComponent<Lexa>();

		GameObject.FindGameObjectsWithTag( "NavMesh" )[0].GetComponent<NavMeshSurface>().BuildNavMesh();
		
		sigDia = GameObject.Find( "SigDia" ).GetComponent<Text>();
		sigDia.enabled = false;

		txtDia = GameObject.Find( "Dia" ).GetComponent<Text>();
		txtDia.text = "Dia: " + oleada;
		sol = GameObject.FindGameObjectsWithTag( "Sol" )[0].GetComponent<Light>();
		sol.color = luz.Evaluate( tiempo );

		// zombies = GameObject.FindGameObjectsWithTag( "Zombie" );

		zombies = new GameObject[3];
		zombies[0] = GameObject.Find( "Zombie Normal" );
		zombies[1] = GameObject.Find( "Zombie Escupidor" );
		zombies[2] = GameObject.Find( "Zombie Explosivo" );

		for ( int i = 0; i < zombies.Length; i++ )
			zombies[i].SetActive( false );

		spawns = new Transform[transform.childCount];

		for ( int i = 0; i < spawns.Length; i++ )
			spawns[i] = transform.GetChild( i );

		IniciarDia();
	}
	
	void Update () {
		tiempo += Time.deltaTime * ( esDia ? 1f : -1f );

		if ( esDia ) {
			if ( tiempo >= 1f )
				tiempo = 1f;

			tiempoSigDia -= Time.deltaTime;

			sigDia.text = "Siguiente dia en " + ( ( int ) tiempoSigDia ) + " segundos";

			if ( tiempoSigDia <= 0f ) {
				SubirNivel();
				esDia = false;
				sigDia.enabled = false;
			}
		} else {
			if ( tiempo <= 0f )
				tiempo = 0f;
		}

		sol.color = luz.Evaluate( tiempo );

		if ( dia && zombiesALaVez > zombiesVivos && zombiesOleadaMax > 0 ) {
			float p = Random.value;
			int tipo = p < zombieNormal ? 0 : p < zombieEscupidor ? 1 : 2;
			GameObject z = Instantiate( zombies[tipo], spawns[(int) Mathf.Floor( Random.value * spawns.Length )].position, zombies[tipo].transform.rotation ) as GameObject;
			z.GetComponent<ZombieController>().SetSpawn( this );
			z.SetActive( true );
			zombiesVivos++;
			zombiesOleadaMax--;

			RecargarBarra();
		}
	}

	void SubirNivel () {
		if ( !err ) {
			zombiesEnOleada += zombiesEnOleadaInc;
			zombiesALaVez += zombiesALaVezInc;
		}

		zombiesOleadaMax = zombiesEnOleada;
		zombiesVivos = 0;
		zombiesMatados = 0;

		RecargarBarra();
	}

	private bool err = true;

	void RecargarBarra () {
		if ( err ) {
			CargarBarra();
			err = false;
		} else {
			float muertos = ( float ) ( ( float ) zombiesMatados / ( float ) zombiesEnOleada );

			if ( muertos == 1f ) {
				esDia = true;
				sigDia.enabled = true;
				tiempoSigDia = tiempoEntreDias;
				lexa.Recuperar();
			}

			marcador.SetValores(
				( float ) ( ( float ) ( zombiesMatados + zombiesVivos ) / ( float ) zombiesEnOleada ),
				muertos
			);
		}
	}

	void CargarBarra() {
		GameObject[] mz = GameObject.FindGameObjectsWithTag( "marcadorZombies" );

		if ( mz.Length == 0 )
			Debug.LogWarning( "Coloque el prefab 'Marcador Zombies' dentro de un Canvas" );
		else
			marcador = mz[0].GetComponent<MarcadorZombies>();
	}

	public void IniciarDia () {
		SubirNivel();
		dia = true;
	}

	public void FinDelDia () {
		dia = false;
	}

	public void ZombieMuerto () {
		zombiesVivos--;
		zombiesMatados++;

		RecargarBarra();
	}
}
