﻿using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    private CapsuleCollider cc;
	private NavMeshAgent zombie;
	private GameObject centro;
	private GameObject player;
	private Animator anim;
    private Spawn spawn;
	public float vida = 200;

	public float getVida(){
		return vida;
	}

    public void SetSpawn (Spawn spawn) {
        this.spawn = spawn;
    }

	public void daniar(float danio){
        if ( vida <= 0 )
            return;

		vida -= danio;
		if(vida <= 0){
            cc.enabled = false;
            spawn.ZombieMuerto();
			anim.SetBool("atacar_jugador", false);
			anim.SetBool("atacar_base", false);
			anim.SetBool("morir", true);
			Destroy(gameObject, 5f);
			zombie.SetDestination(transform.position);
		}
	}

	void Start(){
        cc = GetComponent<CapsuleCollider>();
    	player = GameObject.FindGameObjectsWithTag("Player")[0]; //Obtener jugador por tag;		
    	centro = GameObject.FindGameObjectsWithTag("Centro")[0];
    	anim = GetComponent<Animator>();
        zombie = GetComponent<NavMeshAgent>();
    }	

    void Update () {
        //Debug.log(centro, 'gameObject');
        //Debug.Log(centro);

    	float distance_jugador = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 
    	float distance_base = Vector2.Distance(new Vector2(centro.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 


    	if(vida > 0){
    		if(distance_jugador <= distance_base){    //Ir a lo que esté mas cerca, al jugador o la base	
    			zombie.SetDestination(new Vector3(player.transform.position.x, 1.5f, player.transform.position.z));
    			if(distance_jugador <= 2){
    				zombie.transform.LookAt(player.transform);
    				anim.SetBool("atacar_jugador", true);
    				anim.SetBool("atacar_base", false);
    			}else{
    				anim.SetBool("atacar_jugador", false);
    			}

    		}else{
    			zombie.SetDestination(new Vector3(centro.transform.position.x, 1.5f, centro.transform.position.z));  
    			if(distance_base <= 3){
    				zombie.transform.LookAt(centro.transform);
    				anim.SetBool("atacar_base", true);
    				anim.SetBool("atacar_jugador", false);
    			}else{
    				anim.SetBool("atacar_base", false);
    			}
    		}
    	}
    }

    void FixedUpdate(){

    }
}
