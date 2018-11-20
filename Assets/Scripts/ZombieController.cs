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

	public enum Tipo{
		ZOMBIE1, ZOMBIE2, ZOMBIE3
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
    	tipo = Tipo.ZOMBIE3;

    	if(tipo == Tipo.ZOMBIE3){
    		ataque = ataque * 3;
    		vida = vida * 2;
    	}
    	tiempoSiguienteAtaque = 0;	

    }

   
void Update () {
        //Debug.log(centro, 'gameObject');
        //Debug.Log(centro);
	tiempoSiguienteAtaque -= Time.deltaTime;

	if(player == null)
		return;

	float distance_jugador = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 
	float distance_base = Vector2.Distance(new Vector2(centro.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 


	if(vida > 0){
		Vector3 posicionJugador = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z);
		if(tipo == Tipo.ZOMBIE1 || tipo == Tipo.ZOMBIE2){
			if(distance_jugador <= distance_base){	
				zombie.SetDestination(posicionJugador);
				if(distance_jugador <= 2){
					atacarJugador();
					if(tiempoSiguienteAtaque <= 0){
						if(tipo == Tipo.ZOMBIE2)
							scriptLexa.daniar(-1);
						else
							scriptLexa.daniar(ataque);
			
						tiempoSiguienteAtaque = tiempoEntreAtaque;
					}
				}else{
					detenerAtaque();
				}

			}else{
				zombie.SetDestination(new Vector3(centro.transform.position.x, 1.5f, centro.transform.position.z));  
				if(distance_base <= 3){
					//LLamar funcion para hacer daño a la base
					atacarBase();
				}else{
					detenerAtaque();
				}
			}
		}else if (tipo == Tipo.ZOMBIE3 ){
			zombie.SetDestination(posicionJugador);
			if(distance_jugador <= 20){
				atacarJugador();
				if(tiempoSiguienteAtaque <= 0){
					//scriptLexa.daniar(ataque);
					tiempoSiguienteAtaque = tiempoEntreAtaque;
				}
			}else{
				detenerAtaque();
			}

		}
	}

}

void atacarBase(){
	zombie.transform.LookAt(centro.transform);
	anim.SetBool("atacar_base", true);
	anim.SetBool("atacar_jugador", false);
	zombie.SetDestination(transform.position);
}

void atacarJugador(){
	zombie.transform.LookAt(player.transform);
	anim.SetBool("atacar_base", false);
	anim.SetBool("atacar_jugador", true);
	zombie.SetDestination(transform.position);
}

void detenerAtaque(){
	anim.SetBool("atacar_base", false);
	anim.SetBool("atacar_jugador", false);
}

void FixedUpdate(){

}
}
