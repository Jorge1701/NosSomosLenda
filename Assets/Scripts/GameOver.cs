using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	private GameObject panel;
	private Text texto;

	private bool iniciado = false;

	void Start () {
		panel = GameObject.Find( "Panel" );
		texto = GameObject.Find( "TextGameOver" ).GetComponent<Text>();
	}

	void Update () {
		if ( !iniciado ) {
			panel.SetActive( false );	
			iniciado = true;
		}
	}

	public void Mostrar ( string texto ) {
		panel.SetActive( true );
		this.texto.text = texto;
	}
}
