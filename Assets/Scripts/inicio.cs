using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class inicio : MonoBehaviour {
	public GameObject zombie;
	public NavMeshSurface surface;

	// Use this for initialization
	void Start () {
		surface.BuildNavMesh();
		for(int i=0; i<5; i++){
			GameObject obj = Instantiate(zombie) as GameObject;
			obj.SetActive(true);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
		
		/*if(zombies.Length < 20){
			//Debug.Log(zombies.Length , gameObject);
			GameObject obj = Instantiate(zombie) as GameObject;
			obj.SetActive(true);
		}*/
		
		
	}
}
