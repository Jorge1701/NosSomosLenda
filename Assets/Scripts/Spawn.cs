using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Spawn : MonoBehaviour {

	public int zombiesEnOleada = 20;
	public int zombiesEnOleadaInc = 10;
	public int zombiesALaVez = 4;
	public int zombiesALaVezInc = 2;

	private GameObject zombie;
	private Transform[] spawns;
	private int zombiesOleadaMax = 0;
	private int zombiesVivos = 0;
	private int zombiesMatados = 0;

	private bool dia = false;

	private MarcadorZombies marcador;

	void Start () {
		GameObject[] mz = GameObject.FindGameObjectsWithTag( "marcadorZombies" );

		if ( mz.Length == 0 )
			Debug.LogWarning( "Coloque el prefab 'Marcador Zombies' dentro de un Canvas" );
		else
			marcador = mz[0].GetComponent<MarcadorZombies>();

		GameObject.FindGameObjectsWithTag( "NavMesh" )[0].GetComponent<NavMeshSurface>().BuildNavMesh();;

		zombie = GameObject.FindGameObjectsWithTag( "Zombie" )[0];
		zombie.SetActive( false );

		spawns = new Transform[transform.childCount];

		for ( int i = 0; i < spawns.Length; i++ )
			spawns[i] = transform.GetChild( i );

		IniciarDia();
	}
	
	void Update () {
		if ( dia && zombiesALaVez > zombiesVivos && zombiesOleadaMax > 0 ) {
			GameObject z = Instantiate( zombie, spawns[(int) Mathf.Floor( Random.value * spawns.Length )].position, zombie.transform.rotation ) as GameObject;
			z.GetComponent<ZombieController>().SetSpawn( this );
			z.SetActive( true );
			zombiesVivos++;
			zombiesOleadaMax--;

			RecargarBarra();
		}
	}

	void SubirNivel () {
		zombiesEnOleada += zombiesEnOleadaInc;
		zombiesALaVez += zombiesALaVezInc;

		zombiesOleadaMax = zombiesEnOleada;
		zombiesVivos = 0;
		zombiesMatados = 0;

		RecargarBarra();
	}

	void RecargarBarra () {
		if ( marcador == null )
			return;

		marcador.SetValores(
			( float ) ( ( float ) ( zombiesMatados + zombiesVivos ) / ( float ) zombiesEnOleada ),
			( float ) ( ( float ) zombiesMatados / ( float ) zombiesEnOleada )
		);
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
