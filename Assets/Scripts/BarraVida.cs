using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarraVida : MonoBehaviour {

	public Scrollbar HealthBar;
	private float max = 1f;
	public float Health = 100;

	public void Damage(float value)
	{
		Health -= value;
		HealthBar.size = Health / max;
	}

	public void Max ( float max ) {
		this.max = max;
	}

	public void Vida ( float vida ) {
		Health = vida;
		HealthBar.size = Health / max;
	}

}
