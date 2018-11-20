using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgresoCura : MonoBehaviour {

    public Image Progreso;
    public float tiempo = 5f;
    private float contadorTiempo;
    public Text porcentaje;
   
	void Start () {
        contadorTiempo = 0;
    }
	
	void Update () {
        if (contadorTiempo <= tiempo) {
            contadorTiempo = contadorTiempo + Time.deltaTime;
            Progreso.fillAmount = contadorTiempo / tiempo;
            porcentaje.text = (Convert.ToInt32(100 * Progreso.fillAmount)).ToString()+"%";

        }
	}
}
