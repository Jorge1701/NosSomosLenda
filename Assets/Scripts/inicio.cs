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
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
		
		if(zombies.Length < 10){
			//Debug.Log(zombies.Length , gameObject);
			Instantiate(zombie);
		}
		
		
	}
}
