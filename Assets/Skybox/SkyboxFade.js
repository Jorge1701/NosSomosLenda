#pragma strict

var encendida : boolean;
public var linterna : Light;

var material1 : Material;
var material2 : Material;
 var Duracion : float;
 var LerpSky : boolean = false;
 var velocidad : float = 0.1; //velocidad con la que se acerca la noche y el dia
 private var dia : boolean = true;
 private var noche : boolean = false;
 var ExtiendeDia : int = 6; //tiempo que dura el dia
 var ExtiendeNoche : int = -6; //tiempo que dura la noche


function Start () {

		GetComponent.<Renderer>().material = material1;

}function Update () {

		
	if(LerpSky)		{	
	
		if(Duracion > ExtiendeDia && dia){
		
			Duracion = ExtiendeDia;
			dia = false;
			noche = true;
            encendida = !encendida;
		    if (encendida){
			    linterna.enabled = false;
		    }
		
		
		}if(Duracion <= ExtiendeNoche && noche){
		
			Duracion = ExtiendeNoche;
			dia = true;
        	linterna.enabled = true;
		
		
		}if(dia){
		
			Duracion += Time.deltaTime * velocidad ;
			GetComponent.<Renderer>().material.Lerp (material1, material2, Duracion); // El Render da la suavidad de transicion
		
		}if(noche){
		
			Duracion -= Time.deltaTime * velocidad ;
			GetComponent.<Renderer>().material.Lerp (material1, material2, Duracion);
		
		}
	}	
		
}