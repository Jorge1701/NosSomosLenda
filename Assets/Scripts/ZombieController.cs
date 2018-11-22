using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

	private CapsuleCollider cc;
	private NavMeshAgent zombie;
	private GameObject centro;
	private GameObject player;
	private Animator anim;
	private Spawn spawn;
	public float vida = 200;
	public float ataque = 10;
	private Lexa scriptLexa;
	public float tiempoEntreAtaque = 2;
	private float tiempoSiguienteAtaque;
	public GameObject particulasSangre;
	private Escupitajo escupitajo;
	public GameObject dna;
	public GameObject explosion;
	private bool atacando = false;

	public enum Tipo{
		NORMAL, EXPLOSIVO, ESCUPIDOR
	};

	public Tipo tipo;

	public float getVida(){
		return vida;
	}

	public void SetSpawn (Spawn spawn) {
		this.spawn = spawn;
	}

	public void daniar(float danio){
		if ( vida <= 0 ){
			cc.enabled = false;
			spawn.ZombieMuerto();
			anim.SetBool("atacar_jugador", false);
			anim.SetBool("atacar_base", false);
			anim.SetBool("morir", true);
			Destroy(gameObject, 5f);
			if(tipo == Tipo.NORMAL){
				if(Random.value < .5)
					Instantiate(dna, transform.position, dna.transform.rotation);
			}else{
				Instantiate(dna, transform.position, dna.transform.rotation);		
			}

			zombie.SetDestination(transform.position);
			return;
		}
		
		vida -= danio;
		GameObject sangre = GameObject.Instantiate(particulasSangre, transform.position, particulasSangre.transform.rotation) as GameObject;
		Destroy(sangre, 2f);
		
	}

	void Start(){
		cc = GetComponent<CapsuleCollider>();
    	player = GameObject.FindGameObjectsWithTag("Player")[0]; //Obtener jugador por tag;		
    	centro = GameObject.FindGameObjectsWithTag("Centro")[0];
    	anim = GetComponent<Animator>();
    	zombie = GetComponent<NavMeshAgent>();
    	scriptLexa = player.GetComponent<Lexa>();

    	if(tipo == Tipo.ESCUPIDOR){
    		ataque = ataque * 3;
    		vida = vida * 2;
    		escupitajo = GetComponent<Escupitajo>();
    		tiempoEntreAtaque = 5;
    	}
    	tiempoSiguienteAtaque = 0;	

    }

   
void Update () {
        //Debug.log(centro, 'gameObject');
        //Debug.Log(centro);
	tiempoSiguienteAtaque -= Time.deltaTime;

	Vector3 posicionCentro = new Vector3(centro.transform.position.x, 2.0f, centro.transform.position.z);
	
	if(zombie == null)
		return;

	if(player != null){
		float distance_jugador = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 
		float distance_base = Vector2.Distance(new Vector2(centro.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 
		
		if(vida > 0){
			Vector3 posicionJugador = new Vector3(player.transform.position.x, 2.0f, player.transform.position.z);

			if(tipo == Tipo.NORMAL || tipo == Tipo.EXPLOSIVO){
				if(distance_jugador <= distance_base){	
					
					zombie.SetDestination(posicionJugador);
					
					if(distance_jugador <= 2){
						if(tiempoSiguienteAtaque <= 0){
							if(tipo == Tipo.EXPLOSIVO){
								tirarBomba();
								scriptLexa.daniar(-1);
							}else{
								atacarJugador();
								scriptLexa.daniar(ataque);
							}
							tiempoSiguienteAtaque = tiempoEntreAtaque;
						}
					}else{
						detenerAtaque();
					}

				}else{
					
					zombie.SetDestination(posicionCentro);  
					
					if(distance_base <= 3){
						atacarBase();
					}else{
						detenerAtaque();
					}
				}
			}else if (tipo == Tipo.ESCUPIDOR ){
				
				zombie.SetDestination(posicionJugador);
				
				if(distance_jugador <= 20){
					if(tiempoSiguienteAtaque <= 0){
						atacarJugador();
						escupitajo.Escupir();
						tiempoSiguienteAtaque = tiempoEntreAtaque;
					}
				}else{
					detenerAtaque();
				}

			}
		}
	}else{
		detenerAtaque();
		zombie.SetDestination(posicionCentro);
	}

}

void atacarBase(){
	if(!atacando){
		zombie.transform.LookAt(centro.transform);
		zombie.SetDestination(transform.position);
		anim.SetBool("atacar_base", true);
		anim.SetBool("atacar_jugador", false);
		atacando = true;
	}
	
}

void atacarJugador(){
	if(!atacando){
		zombie.transform.LookAt(player.transform);
		zombie.SetDestination(transform.position);
		anim.SetBool("atacar_base", false);
		anim.SetBool("atacar_jugador", true);
		atacando = true;
	}
}

void tirarBomba(){
	Instantiate(explosion, transform.position, explosion.transform.rotation);
	Destroy(zombie);
}

void detenerAtaque(){
	if(atacando){
		anim.SetBool("atacar_base", false);
		anim.SetBool("atacar_jugador", false);
		atacando = false;
	}
}
	

void FixedUpdate(){

}
}
