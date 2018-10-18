using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class ZombieController : MonoBehaviour {

    //public Camera cam;
    public NavMeshAgent zombie;
    public ThirdPersonCharacter character;

    public Vector3 bunker = new Vector3(-13.8f,1f,-11.5f);

    private GameObject player;


    void Start(){
        player = GameObject.FindGameObjectsWithTag("Player")[0]; //Obtener jugador por nombre;		
        zombie.updateRotation = false;
    }

    void Update () {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            { 
                zombie.SetDestination(hit.point); // Para ir a donde se hizo click.
            }
        }*/


        float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(zombie.transform.position.x, zombie.transform.position.z)); 
        if(distance <= 10){    //Si el jugador esta cerca ir hacia el.
            zombie.SetDestination(new Vector3(player.transform.position.x, 1f, player.transform.position.z));
        }else{
            zombie.SetDestination(bunker);  //Posicion del TIP;
        }

        if(zombie.remainingDistance > zombie.stoppingDistance){ //Animar mientras le quede camino por recorrer, cuando llegue detener.
            character.Move(zombie.desiredVelocity, false, false);    
        }else{
            character.Move(Vector3.zero, false, false);
        }

    }

    void FixedUpdate(){
     //Actualizaciones siempre cada la misma cantidad de tiempo;   
    }
}
