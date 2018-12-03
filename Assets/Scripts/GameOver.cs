using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	private GameObject panel;
	private Text texto;
	private Animator anim;

	void Start () {
		panel = GameObject.Find( "Panel" );
		texto = GameObject.Find( "TextGameOver" ).GetComponent<Text>();
	}

	void Update () {
		panel.SetActive( false );
	}

	public void Mostrar ( string texto ) {
		panel.SetActive( true );
		this.texto.text = texto;
	}
}
