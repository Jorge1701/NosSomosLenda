using UnityEngine.UI;
using UnityEngine;

public class MarcadorZombies : MonoBehaviour {

	public Color colTotal;
	public Color colVivos;
	public Color colMuertos;

	private Slider sVivos;
	private Slider sMuertos;

	void Start () {
		GameObject.Find( "bgTotal" ).GetComponent<Image>().color = colTotal;
		GameObject.Find( "bgVivos" ).GetComponent<Image>().color = colVivos;
		GameObject.Find( "bgMuertos" ).GetComponent<Image>().color = colMuertos;

		sVivos = GameObject.Find( "Vivos" ).GetComponent<Slider>();
		sMuertos = GameObject.Find( "Muertos" ).GetComponent<Slider>();
	}

	public void Reset () {
		sVivos.value = 0;
		sMuertos.value = 0;
	}

	public void SetValores ( float vivos, float muertos ) {
		sVivos.value = vivos;
		sMuertos.value = muertos;
	}
}
